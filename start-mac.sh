#!/bin/bash

echo "🚀 啟動 ChatDemo 專案..."

# 檢查 Ollama 是否運行
if ! curl -s http://localhost:11434/api/tags >/dev/null 2>&1; then
    echo "⚠️  Ollama 服務未運行，正在啟動..."
    ollama serve &
    sleep 3
fi

# 檢查是否已下載模型
if ! ollama list | grep -q "llama3.2"; then
    echo "📥 下載 AI 模型..."
    ollama pull llama3.2:latest
fi

echo "✅ Ollama 服務已準備就緒"

# 啟動後端
echo "🔧 啟動後端服務..."
cd ChatDemo
dotnet run &
BACKEND_PID=$!

# 等待後端啟動
echo "⏳ 等待後端服務啟動..."
sleep 5

# 啟動前端
echo "🎨 啟動前端服務..."
cd "$(dirname "$0")/chatapp"
npm run dev &
FRONTEND_PID=$!

echo "✅ 所有服務已啟動！"
echo ""
echo "🌐 服務網址："
echo "- 前端應用: http://localhost:5173"
echo "- 後端 API: http://localhost:5006"
echo "- Swagger 文件: http://localhost:5006/swagger"
echo ""
echo "按 Ctrl+C 停止所有服務"

# 等待用戶中斷
trap "echo '🛑 正在停止服務...'; kill $BACKEND_PID $FRONTEND_PID 2>/dev/null; exit" INT
wait 