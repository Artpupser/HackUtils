package main

import (
	"bufio"
	"encoding/json"
	"fmt"
	"io"
	"net/http"
	"net/url"
	"os"
	"strings"
)

type TokenResponse struct {
	AccessToken string `json:"access_token"`
	TokenType   string `json:"token_type"`
	ExpiresIn   int    `json:"expires_in"`
}

type YandexUser struct {
	ID           string `json:"id"`
	Login        string `json:"login"`
	DefaultEmail string `json:"default_email"`
}

var (
	clientID     string
	clientSecret string
	redirectURI  string
)

func loadEnv(path string) error {
	file, err := os.Open(path)
	if err != nil {
		return err
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := strings.TrimSpace(scanner.Text())

		if line == "" || strings.HasPrefix(line, "#") {
			continue
		}

		parts := strings.SplitN(line, "=", 2)
		if len(parts) != 2 {
			continue
		}

		key := strings.TrimSpace(parts[0])
		val := strings.TrimSpace(parts[1])
		os.Setenv(key, val)
	}
	return scanner.Err()
}

func main() {
	clientID = "376a55260ae4463592a1f92f16793d81"
	clientSecret = "c976812b1eca4d28bcaf0e0c46ab34a6"
	redirectURI = "http://localhost:8080/yandex/callback"

	if clientID == "" || clientSecret == "" {
		panic("Env variables not set")
	}

	http.HandleFunc("/yandex/login", yandexLogin)
	http.HandleFunc("/yandex/callback", yandexCallback)

	fmt.Println("Server started on http://localhost:8080")
	http.ListenAndServe(":8080", nil)
}

func yandexLogin(w http.ResponseWriter, r *http.Request) {
	authURL := fmt.Sprintf(
		"https://oauth.yandex.ru/authorize?response_type=code&client_id=%s&redirect_uri=%s",
		clientID,
		url.QueryEscape(redirectURI),
	)

	http.Redirect(w, r, authURL, http.StatusTemporaryRedirect)
}

func yandexCallback(w http.ResponseWriter, r *http.Request) {
	code := r.URL.Query().Get("code")

	if code == "" {
		http.Error(w, "Code not found", http.StatusBadRequest)
		return
	}

	token, err := getToken(code)
	if err != nil {
		http.Error(w, err.Error(), 500)
		return
	}

	user, err := getUser(token)
	if err != nil {
		http.Error(w, err.Error(), 500)
		return
	}

	fmt.Fprintf(w,
		"User ID: %s<br>Login: %s<br>Email: %s",
		user.ID,
		user.Login,
		user.DefaultEmail,
	)
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

	var token TokenResponse
	err = json.Unmarshal(body, &token)
	if err != nil {
		return "", err
	}

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

	var user YandexUser
	err = json.Unmarshal(body, &user)
	if err != nil {
		return nil, err
	}

	return &user, nil
}
