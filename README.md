<p align="center">
<!-- <img src="docs/_images/Afdian.Sdk.png" alt="Afdian.Sdk"> -->
</p>
<h1 align="center">Afdian.Sdk</h1>

> :cake: 爱发电 非官方 .NET SDK

[![repo size](https://img.shields.io/github/repo-size/yiyungent/Afdian.Sdk.svg?style=flat)]()
[![LICENSE](https://img.shields.io/github/license/yiyungent/Afdian.Sdk.svg?style=flat)](https://github.com/yiyungent/Afdian.Sdk/blob/master/LICENSE)
[![nuget](https://img.shields.io/nuget/v/Afdian.Sdk.svg?style=flat)](https://www.nuget.org/packages/Afdian.Sdk/)
[![downloads](https://img.shields.io/nuget/dt/Afdian.Sdk.svg?style=flat)](https://www.nuget.org/packages/Afdian.Sdk/)
[![爱发电](https://afdian.moeci.com/1/badge.svg)](https://afdian.net/@yiyun)
[![QQ Group](https://img.shields.io/badge/QQ%20Group-894031109-deepgreen)](https://jq.qq.com/?_wv=1027&k=q5R82fYN)


## 介绍

Afdian.Sdk: 爱发电 非官方 .NET SDK
 

## 使用


```bash
dotnet add package Afdian.Sdk
```

```csharp
using Afdian.Sdk;


AfdianClient afdianClient = new AfdianClient(userId: "", token: "");
string jsonStr1 = afdianClient.Ping();
string jsonStr2 = afdianClient.QueryOrder(page: 1);
string jsonStr3 = afdianClient.QuerySponsor(page: 1);

var jsonModel1 = afdianClient.QueryOrderModel(page: 1);
var jsonModel2 = afdianClient.QuerySponsorModel(page: 1);

// ...还有一些API, 例如: QueryOrderAsync()
```

## Afdian.Server

> 基于 `Afdian.Sdk` 的 非官方 爱发电 在线辅助服务

### 功能

- [x] [申请爱发电 徽章](https://afdian.moeci.com/swagger)
- [x] 收到赞助 发送 Telegram 通知 (基于 爱发电 Webhook)

### Docker 快速运行

> Docker Compose (推荐)

```bash
# 注意: 一定要下载 配置文件: appsettings.json
wget https://raw.githubusercontent.com/yiyungent/Afdian.Sdk/main/src/Afdian.Server/appsettings.json

# 仓库根目录 docker-compose.yml
wget https://raw.githubusercontent.com/yiyungent/Afdian.Sdk/main/docker-compose.yml

docker-compose up -d
```

## 开发

### 单元测试 Tests

> `secrets.json`

```json
{
  "SecretsKeys": {
    "AfdianToken": "",
    "AfdianUserId": ""
  }
}
```

## Related Projects

- [yiyungent/afdian-action: 🍰 自动更新 爱发电 赞助列表 | GitHub Action](https://github.com/yiyungent/afdian-action)

## Donate

Afdian.Sdk is an MIT licensed open source project and completely free to use. However, the amount of effort needed to maintain and develop new features for the project is not sustainable without proper financial backing.

We accept donations through these channels:
- <a href="https://afdian.net/@yiyun" target="_blank">爱发电</a>

## Author

**Afdian.Sdk** © [yiyun](https://github.com/yiyungent), Released under the [MIT](./LICENSE) License.<br>
Authored and maintained by yiyun with help from contributors ([list](https://github.com/yiyungent/Afdian.Sdk/contributors)).

> GitHub [@yiyungent](https://github.com/yiyungent) Gitee [@yiyungent](https://gitee.com/yiyungent)

