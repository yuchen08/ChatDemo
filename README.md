# ChatDemo - AI 聊天機器人專案

## 📋 專案簡介

ChatDemo 是一個整合 AI 聊天功能的會員系統，採用前後端分離架構。前端使用 Vue.js 3 搭配 Tailwind CSS 建立現代化的使用者介面，後端使用 ASP.NET Core 8 提供 RESTful API 服務，並整合 Ollama 本地 AI 模型進行智能對話。

## 🏗️ 技術架構

### 後端技術棧
- **.NET 8** - 主要開發框架
- **ASP.NET Core Web API** - RESTful API 服務
- **Dapper** - 輕量級 ORM 框架
- **SQL Server** - 資料庫
- **BCrypt.Net-Next** - 密碼加密
- **OllamaSharp** - Ollama AI 模型整合
- **Swagger** - API 文件生成

### 前端技術棧
- **Vue.js 3** - 前端框架
- **Vue Router** - 路由管理
- **Pinia** - 狀態管理
- **Tailwind CSS** - CSS 框架
- **Vite** - 建構工具
- **Axios** - HTTP 客戶端

## 🚀 功能特色

- ✅ 會員註冊與登入系統
- ✅ 密碼安全加密儲存
- ✅ AI 智能對話功能
- ✅ 響應式設計介面
- ✅ 即時問答互動
- ✅ 現代化 UI/UX 設計

## 📦 系統需求

### 必要軟體
- **.NET 8 SDK**
- **Node.js 18+**
- **SQL Server** (或 SQL Server Express)
- **Ollama** (本地 AI 模型服務)

### 推薦環境
- **Visual Studio 2022** 或 **VS Code**
- **SQL Server Management Studio** (SSMS)

## 🛠️ 安裝與設定

### 1. 克隆專案
```bash
git clone <repository-url>
cd ChatDemo
```

### 2. 後端設定

#### 資料庫設定
1. 確保 SQL Server 正在運行
2. 建立資料庫 `ChatDemoMembers`
3. 修改 `appsettings.json` 中的連線字串：
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ChatDemoMembers;User Id=sa;Password=YourPassword;"
  }
}
```

#### 安裝 Ollama
1. 下載並安裝 [Ollama](https://ollama.ai/)
2. 啟動 Ollama 服務
3. 下載所需模型：
```bash
ollama pull llama3.2:latest
```

#### 執行後端
```bash
cd ChatDemo
dotnet restore
dotnet run
```

後端服務將在 `http://localhost:5006` 啟動

### 3. 前端設定

#### 安裝依賴
```bash
cd chatapp
npm install
```

#### 執行前端
```bash
npm run dev
```

前端應用將在 `http://localhost:5173` 啟動

## 📁 專案結構

```
ChatDemo/
├── ChatDemo/                    # 後端專案
│   ├── Controllers/            # API 控制器
│   │   ├── DapperMemberController.cs  # 會員管理 API
│   │   └── OllamaController.cs        # AI 對話 API
│   ├── Models/                 # 資料模型
│   │   └── Members.cs         # 會員模型
│   ├── Services/              # 業務邏輯服務
│   │   ├── DapperDatabaseService.cs   # 資料庫服務
│   │   ├── DapperMemberService.cs     # 會員服務
│   │   └── OllamaService.cs          # AI 服務
│   ├── appsettings.json       # 應用程式設定
│   └── Program.cs             # 應用程式入口
├── chatapp/                   # 前端專案
│   ├── src/
│   │   ├── components/        # Vue 元件
│   │   │   ├── ChatApp.vue    # 聊天主頁面
│   │   │   ├── Login.vue      # 登入頁面
│   │   │   └── Register.vue   # 註冊頁面
│   │   ├── stores/           # Pinia 狀態管理
│   │   │   └── user.js       # 使用者狀態
│   │   ├── router/           # 路由設定
│   │   │   └── index.js      # 路由配置
│   │   └── App.vue           # 根元件
│   ├── package.json          # 前端依賴
│   └── vite.config.js        # Vite 設定
└── README.md                 # 專案說明文件
```

## 🔧 API 端點

### 會員管理 API
- `POST /api/dappermember/register` - 會員註冊
- `POST /api/dappermember/login` - 會員登入

### AI 對話 API
- `GET /api/ollama/question/{question}` - 取得 AI 回答

## 🎯 使用方式

1. **註冊帳號**：首次使用請先註冊會員帳號
2. **登入系統**：使用註冊的帳號密碼登入
3. **開始對話**：在聊天介面輸入問題，AI 將提供智能回答
4. **登出系統**：完成使用後請登出系統

## 🔒 安全性

- 使用 BCrypt 進行密碼雜湊加密
- 實作 CORS 政策保護 API
- 輸入驗證與錯誤處理
- 安全的資料庫連線

## 🐳 Docker 支援

專案包含 Dockerfile，可進行容器化部署：

```bash
# 建立 Docker 映像
docker build -t chatdemo .

# 執行容器
docker run -p 5006:5006 chatdemo
```

## 📝 開發者資訊

- **作者**：許浴宸
- **電子郵件**：hsuyuchen08@gmail.com
- **學歷**：雲林科技大學畢業
- **目標**：尋找程式設計相關研發替代役職缺

## 🤝 貢獻指南

1. Fork 專案
2. 建立功能分支 (`git checkout -b feature/AmazingFeature`)
3. 提交變更 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 開啟 Pull Request

## 📄 授權條款

此專案採用 MIT 授權條款 - 詳見 [LICENSE](LICENSE) 文件

## 🆘 常見問題

### Q: 無法連接到資料庫？
A: 請確認 SQL Server 服務正在運行，並檢查 `appsettings.json` 中的連線字串設定。

### Q: AI 對話沒有回應？
A: 請確認 Ollama 服務正在運行，並已下載所需的 AI 模型。

### Q: 前端無法連接到後端 API？
A: 請確認後端服務在 `http://localhost:5006` 正常運行，並檢查 CORS 設定。

## 📞 聯絡資訊

如有任何問題或建議，請透過以下方式聯絡：

- **Email**: hsuyuchen08@gmail.com
- **GitHub**: yuchen08

---

⭐ 如果這個專案對您有幫助，請給我們一個星標！ 