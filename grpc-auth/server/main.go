package main

import (
	"context"
	"encoding/json"
	"fmt"
	"io"
	"log"
	"net"
	"net/http"
	"net/url"

	pb "grpc-auth/proto"

	"google.golang.org/grpc"
)

var (
	clientID     = "376a55260ae4463592a1f92f16793d81"
	clientSecret = "c976812b1eca4d28bcaf0e0c46ab34a6"
	redirectURI  = "http://localhost:8080/yandex/callback"
)

type server struct {
	pb.UnimplementedAuthServiceServer
}

type TokenResponse struct {
	AccessToken string `json:"access_token"`
}

type YandexUser struct {
	ID           string `json:"id"`
	Login        string `json:"login"`
	DefaultEmail string `json:"default_email"`
}

func startHTTPCallbackServer() {
	http.HandleFunc("/yandex/callback", func(w http.ResponseWriter, r *http.Request) {
		code := r.URL.Query().Get("code")
		fmt.Println("🔥 YANDEX RETURNED CODE:")
		fmt.Println(code)
		fmt.Fprintf(w, "Login success! Return to console.")
	})

	go func() {
		log.Println("🌐 HTTP callback server started on :8080")
		err := http.ListenAndServe(":8080", nil)
		if err != nil {
			log.Fatal(err)
		}
	}()
}

func (s *server) GetYandexAuthURL(ctx context.Context, e *pb.Empty) (*pb.AuthURLResponse, error) {
	authURL := fmt.Sprintf(
		"https://oauth.yandex.ru/authorize?response_type=code&client_id=%s&redirect_uri=%s",
		clientID,
		url.QueryEscape(redirectURI),
	)

	return &pb.AuthURLResponse{Url: authURL}, nil
}

func (s *server) ExchangeCode(ctx context.Context, req *pb.CodeRequest) (*pb.UserResponse, error) {
	token, err := getToken(req.Code)
	if err != nil {
		return nil, err
	}

	user, err := getUser(token)
	if err != nil {
		return nil, err
	}

	return &pb.UserResponse{
		Id:    user.ID,
		Login: user.Login,
		Email: user.DefaultEmail,
	}, nil
}

func getToken(code string) (string, error) {
	data := url.Values{}
	data.Set("grant_type", "authorization_code")
	data.Set("code", code)
	data.Set("client_id", clientID)
	data.Set("client_secret", clientSecret)
	data.Set("redirect_uri", redirectURI)

	resp, err := http.PostForm("https://oauth.yandex.ru/token", data)
	if err != nil {
		return "", err
	}
	defer resp.Body.Close()

	body, _ := io.ReadAll(resp.Body)

	fmt.Println("TOKEN RESPONSE:", string(body))

	var token TokenResponse
	json.Unmarshal(body, &token)

	return token.AccessToken, nil
}

func getUser(token string) (*YandexUser, error) {
	req, _ := http.NewRequest("GET", "https://login.yandex.ru/info", nil)
	req.Header.Set("Authorization", "OAuth "+token)

	client := &http.Client{}
	resp, err := client.Do(req)
	if err != nil {
		return nil, err
	}
	defer resp.Body.Close()

	body, _ := io.ReadAll(resp.Body)

	fmt.Println("USER RESPONSE:", string(body)) // лог для дебага

	var user YandexUser
	json.Unmarshal(body, &user)

	return &user, nil
}

func main() {
	startHTTPCallbackServer()
	lis, err := net.Listen("tcp", ":50051")
	if err != nil {
		log.Fatal(err)
	}

	grpcServer := grpc.NewServer()
	pb.RegisterAuthServiceServer(grpcServer, &server{})

	log.Println("🚀 gRPC Auth server started on :50051")
	grpcServer.Serve(lis)
}
