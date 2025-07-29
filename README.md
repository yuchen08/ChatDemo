# ChatDemo 專案簡介

這是一個採用分層架構的 .NET 8 Web API 專案，支援會員註冊/登入與 AI 問答功能，並可一鍵切換 Mock（假資料）或真實服務，適合新手學習與開發。

---

## 目錄結構
```
ChatDemo/
├── Contracts/           # 介面定義
├── Api/                 # 控制器
├── Application/         # 業務邏輯
├── Implementation/      # 真實與Mock服務
├── Models/              # 資料結構
├── Program.cs           # 入口設定
├── 架構說明.md           # 詳細架構與教學
└── 教學詳細註解.md       # 進階註解與範例
```

---

## 如何切換 Mock/真實服務？
1. 打開 `ChatDemo/Program.cs`
2. 找到 `bool useMock = true;`
3. `true`=用假資料，`false`=用真實服務
4. 存檔後重啟服務即可切換

---

## 啟動步驟
1. 安裝 [.NET 8 SDK](https://dotnet.microsoft.com/zh-tw/download/dotnet/8.0)
2. 在專案根目錄執行：
   ```sh
   dotnet restore
   dotnet build
   dotnet run
   ```
3. 預設網址：http://localhost:5006
4. Swagger UI：http://localhost:5006/swagger

---

## 常見問題
- Mock 會員/AI 只存在記憶體，重啟就消失
- 切換 Mock 只需改 `useMock` 變數
- 詳細說明請見 `架構說明.md`

---

## 進階教學
請參考 `教學詳細註解.md` 與 `架構說明.md`，有完整中文註解與範例。 