package main

import (
	"context"
	"fmt"
	"log"

	"google.golang.org/grpc"

	pb "grpc-auth/proto"
)

func main() {
	conn, err := grpc.Dial("localhost:50051", grpc.WithInsecure())
	if err != nil {
		log.Fatal(err)
	}
	defer conn.Close()

	client := pb.NewAuthServiceClient(conn)

	resp, err := client.GetYandexAuthURL(context.Background(), &pb.Empty{})
	if err != nil {
		log.Fatal(err)
	}
	fmt.Println("Ссылка для авторизации через Яндекс:")
	fmt.Println(resp.Url)

	var code string
	fmt.Print("Введите код авторизации: ")
	fmt.Scanln(&code)

	userResp, err := client.ExchangeCode(context.Background(), &pb.CodeRequest{Code: code})
	if err != nil {
		log.Fatal(err)
	}

	fmt.Println("Информация о пользователе:")
	fmt.Printf("ID: %s\nLogin: %s\nEmail: %s\n", userResp.Id, userResp.Login, userResp.Email)
}
