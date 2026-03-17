@echo off
echo Startuji infrastrukturu (DB, Redis, RabbitMQ)...
docker compose up -d db redis rabbitmq
echo Infrastruktura bezi.
pause