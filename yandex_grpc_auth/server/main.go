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

	pb "yandex_grpc_auth/proto"

	"google.golang.org/grpc"
)

var (
	clientID     = "376a55260ae4463592a1f92f16793d81"
	clientSecret = "c976812b1eca4d28bcaf0e0c46ab34a6"
	redirectURI  = "http://localhost/api/yandex/callback"
)

type server struct {
	pb.UnimplementedYandexAuthServiceServer
}

type TokenResponse struct {
	AccessToken string `json:"access_token"`
}

type YandexUser struct {
	ID           string `json:"id"`
	Login        string `json:"login"`
	DefaultEmail string `json:"default_email"`
}

func (s *server) GetYandexAuthURL(ctx context.Context, e *pb.Empty) (*pb.YandexAuthURLResponse, error) {
	authURL := fmt.Sprintf(
		"https://oauth.yandex.ru/authorize?response_type=code&client_id=%s&redirect_uri=%s",
		clientID,
		url.QueryEscape(redirectURI),
	)

	return &pb.YandexAuthURLResponse{Url: authURL}, nil
}

func (s *server) ExchangeCode(ctx context.Context, req *pb.YandexCodeRequest) (*pb.YandexUserResponse, error) {
	token, err := getToken(req.Code)
	if err != nil {
		return nil, err
	}

	user, err := getUser(token)
	if err != nil {
		return nil, err
	}

	return &pb.YandexUserResponse{
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
	if err := json.Unmarshal(body, &token); err != nil {
		return "", err
	}

	if token.AccessToken == "" {
		return "", fmt.Errorf("failed to get access_token")
	}

	return token.AccessToken, nil
}

func getUser(token string) (*YandexUser, error) {
	req, err := http.NewRequest("GET", "https://login.yandex.ru/info", nil)
	if err != nil {
		return nil, err
	}

	req.Header.Set("Authorization", "OAuth "+token)

	client := &http.Client{}
	resp, err := client.Do(req)
	if err != nil {
		return nil, err
	}
	defer resp.Body.Close()

	body, _ := io.ReadAll(resp.Body)

	fmt.Println("USER RESPONSE:", string(body))

	var user YandexUser
	if err := json.Unmarshal(body, &user); err != nil {
		return nil, err
	}

	return &user, nil
}

func main() {
	lis, err := net.Listen("tcp", ":5433")
	if err != nil {
		log.Fatal(err)
	}
	grpcServer := grpc.NewServer()
	pb.RegisterYandexAuthServiceServer(grpcServer, &server{})
	log.Println("🚀 gRPC Yandex Auth server started on :5433")
	if err := grpcServer.Serve(lis); err != nil {
		log.Fatal(err)
	}
}
