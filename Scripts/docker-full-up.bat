@echo off
echo Startuji uplne vsechno...
docker compose up -d --build
echo Vse bezi (API na localhost:8080, RabbitMQ na 15672).
pause