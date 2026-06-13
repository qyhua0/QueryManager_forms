# 通用查询数据管理器

## 功能概述
动态加载 JSON 配置的查询报表工具，支持自定义 SQL、多种查询条件（日期选择器、文本模糊查找、下拉选择等），兼容 SQL Server 2000 及以上版本。

---

## 项目结构

```
QueryManager/
├── Program.cs                  # 程序入口
├── QueryManager.csproj         # 项目文件 (.NET 4.8)
├── Models/
│   └── QueryConfig.cs          # 所有数据模型（AppConfig / QueryReport / QueryParameter / ColumnConfig）
├── Services/
│   ├── ConfigService.cs        # JSON配置读写
│   ├── DatabaseService.cs      # 数据库查询（MSSQL兼容）
│   └── ExportService.cs        # Excel / CSV 导出
├── Forms/
│   ├── MainForm.cs             # 主窗体
│   ├── ConnectionManagerForm.cs# 连接管理窗体
│   ├── ReportManagerForm.cs    # 报表JSON编辑窗体
│   └── SqlEditorForm.cs        # 自由SQL编辑器
└── config/
    └── queries.json            # 运行时配置文件
```

---

## 编译运行

### 前置要求
- Visual Studio 2019+ 或 .NET 4.8 SDK
- Windows 操作系统

### 编译步骤
```bash
cd QueryManager
dotnet restore
dotnet build -c Release
dotnet run
```

---

## JSON 配置格式详解

### 顶层结构
```json
{
  "Connections": [ ... ],   // 数据库连接列表
  "Reports": [ ... ]        // 查询报表列表
}
```

### 数据库连接 (ConnectionConfig)
```json
{
  "Id": "conn1",                      // 唯一ID（供报表引用）
  "Name": "生产数据库",               // 显示名称
  "DbType": "MSSQL",                  // MSSQL 或 MSSQL2000
  "ConnectionString": "Server=...;"   // ADO.NET 连接字符串
}
```

**SQL Server 2000 连接字符串建议：**
```
Server=192.168.1.1,1433;Database=MyDB;User Id=sa;Password=xxx;Packet Size=4096;
```

### 查询报表 (QueryReport)
```json
{
  "Id": "rpt1",
  "Name": "订单查询",            // 左侧树状菜单显示名
  "Description": "查询说明",
  "ConnectionId": "conn1",       // 引用连接ID
  "BaseSql": "SELECT * FROM Orders WHERE 1=1",  // 基础SQL（末尾加 WHERE 1=1）
  "Parameters": [ ... ],         // 动态查询条件
  "Columns": [ ... ]             // 列显示配置
}
```

### 查询参数 (QueryParameter)

| 字段 | 说明 |
|------|------|
| Name | 参数名，SQL中用 @Name 引用 |
| Label | 界面显示标签 |
| ControlType | TextBox / DatePicker / ComboBox / CheckBox |
| SqlFragment | 追加到 WHERE 后的 SQL，如 `AND OrderDate >= @StartDate` |
| FuzzySearch | true = 自动将值改为 `%value%`（配合 LIKE 使用） |
| Options | ComboBox 的选项列表 |
| Required | true = 必填 |
| DefaultValue | 默认值 |

### 列配置 (ColumnConfig)

| 字段 | 说明 |
|------|------|
| Field | 数据库字段名（大小写需与查询结果一致） |
| Header | 表头显示文字 |
| Width | 列宽（像素） |
| Format | 格式化字符串，如 `yyyy-MM-dd`、`N2` |
| Align | Left / Center / Right |

---

## 动态 SQL 构建逻辑

程序运行时，针对每个有值的参数：
1. 追加 `SqlFragment` 到 BaseSql 末尾
2. 若 `FuzzySearch=true`，将值自动包裹为 `%value%`
3. 若 `ControlType=DatePicker` 且有勾选，以 `DateTime` 类型传参

**示例：**
- BaseSql: `SELECT * FROM Orders WHERE 1=1`
- 用户输入 StartDate=2023-01-01，CustomerID=ALF
- 最终 SQL: `SELECT * FROM Orders WHERE 1=1 AND OrderDate >= @StartDate AND CustomerID LIKE @CustomerID`
- 参数值: `@StartDate=2023-01-01`, `@CustomerID=%ALF%`

---

## SQL Server 2000 兼容说明

- 使用 `System.Data.SqlClient`（不依赖 Microsoft.Data.SqlClient）
- 查询表名使用 `sysobjects`（而非 INFORMATION_SCHEMA）
- 不使用 `ROW_NUMBER()`、`OFFSET-FETCH` 等 2005+ 语法
- `CommandTimeout = 120` 防止超时
- 连接字符串建议加 `Packet Size=4096`

---

## 快捷键

| 键 | 功能 |
|----|------|
| F5 | 执行查询 |

