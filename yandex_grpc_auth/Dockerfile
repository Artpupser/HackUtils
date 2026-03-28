FROM golang:1.26-alpine AS builder

WORKDIR /app

COPY . .
RUN go mod download

WORKDIR /app/server
RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -o app

FROM gcr.io/distroless/base-debian12

WORKDIR /app

COPY --from=builder /app/server/app .

EXPOSE 5433

CMD ["./app"]