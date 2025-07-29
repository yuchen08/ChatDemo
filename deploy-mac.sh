#!/bin/bash

echo "🚀 開始部署 ChatDemo 專案..."

# 檢查必要工具
echo "📋 檢查必要工具..."
command -v dotnet >/dev/null 2>&1 || { echo "❌ .NET 8 SDK 未安裝，請執行: brew install dotnet"; exit 1; }
command -v node >/dev/null 2>&1 || { echo "❌ Node.js 未安裝，請執行: brew install node"; exit 1; }
command -v ollama >/dev/null 2>&1 || { echo "❌ Ollama 未安裝，請執行: brew install ollama"; exit 1; }

echo "✅ 所有必要工具已安裝"

# 後端設定
echo "🔧 設定後端..."
cd ChatDemo
dotnet restore

# 檢查是否已安裝 SQLite 套件
if ! dotnet list package | grep -q "Microsoft.EntityFrameworkCore.Sqlite"; then
    echo "📦 安裝 SQLite 套件..."
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.Design
fi

# 前端設定
echo "🎨 設定前端..."
cd ../chatapp
npm install

echo "✅ 部署完成！"
echo ""
echo "📝 接下來請依序執行："
echo "1. 啟動 Ollama 服務: ollama serve"
echo "2. 下載 AI 模型: ollama pull llama3.2:latest"
echo "3. 執行後端服務: cd ChatDemo && dotnet run"
echo "4. 執行前端服務: cd chatapp && npm run dev"
echo ""
echo "🌐 服務啟動後："
echo "- 後端 API: http://localhost:5006"
echo "- 前端應用: http://localhost:5173"
echo "- Swagger 文件: http://localhost:5006/swagger" 