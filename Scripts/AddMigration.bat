@echo off
dotnet ef migrations add %1 --project ..\CryptoGuard.Infrastructure --startup-project ..\CryptoGuard.API