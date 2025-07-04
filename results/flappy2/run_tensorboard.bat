@echo off
cd /d %~dp0
tensorboard --logdir . --port 6006
pause
