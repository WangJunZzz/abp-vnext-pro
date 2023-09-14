# 异常处理

ABP 提供了用于处理 Web 应用程序异常的标准模型.

- 自动处理所有异常.如果是 API/AJAX 请求,会向客户端返回一个标准格式化后的错误消息 .
- 自动隐藏内部详细错误并返回标准错误消息.
- 为异常消息的本地化提供一种可配置的方式.
- 自动为标准异常设置\*HTTP 状态代码,并提供可配置选项,以映射自定义异常.

## 自动处理异常

当满足下面任意一个条件时,`AbpExceptionFilter` 会处理此异常:

- 当 controller action 方法返回类型是 object result(而不是 view result)并有异常抛出时.
- 当一个请求为 AJAX(Http 请求头中`X-Requested-With`为`XMLHttpRequest`)时.
- 当客户端接受的返回类型为`application/json`(Http 请求头中`accept` 为`application/json`)时.

## 错误消息格式

```json
{
  "error": {
    "code": "App:010042",
    "message": "This topic is locked and can not add a new message",
    "details": "A more detailed info about the error..."
  }
}
```

- 错误代码(code)是异常信息中一个有唯一值并可选的字符串值.抛出的异常应实现`IHasErrorCode` 接口来填充该字段.
- 错误的详细信息(Details) 是可选属性.抛出的异常应实现`IHasErrorDetails` 接口来填充该字段.

### 验证错误

当抛出的异常实现`IHasValidationErrors` 接口时,validationErrors 是一个可被填充的标准字段.示例 JSON 如下:

```json
{
  "error": {
    "code": "App:010046",
    "message": "Your request is not valid, please correct and try again!",
    "validationErrors": [
      {
        "message": "Username should be minimum length of 3.",
        "members": ["userName"]
      },
      {
        "message": "Password is required",
        "members": ["password"]
      }
    ]
  }
}
```

`AbpValidationException`已经实现了`IHasValidationErrors`接口,当请求输入无效时,框架会自动抛出此错误. 因此,除非你有自定义的验证逻辑,否则不需要处理验证错误.

## 业务异常

大多数异常都是业务异常.可以通过使用`IBusinessException` 接口来标记异常为业务异常.

`BusinessException` 除了实现`IHasErrorCode`,`IHasErrorDetails` ,`IHasLogLevel` 接口外,还实现了`IBusinessException` 接口.其默认日志级别为`Warning`.

通常你会将一个错误代码关联至特定的业务异常.例如:

```C#
throw new BusinessException(QaErrorCodes.CanNotVoteYourOwnAnswer);
```

`QaErrorCodes.CanNotVoteYourOwnAnswer` 是一个字符串常量. 建议使用下面的错误代码格式:

```
<code-namespace>:<error-code>
```

code-namespace,应在指定的模块/应用层中保证其唯一.例如:

```
Volo.Qa:010002
```

`Volo.Qa`在这是作为`code-namespace`. `code-namespace` 同样可以在 本地化 异常信息时使用.

- 你可以直接抛出一个 `BusinessException` 异常,或者需要时可以从该类派生你自己的 Exception 类型.
- 对于`BusinessException` 类型,其所有属性都是可选的.但是通常会设置`ErrorCode`或`Message`属性.

## 使用错误代码

通过使用错误代码的方式来处理本地化,而不是在抛出异常的时候.

首先,在模块配置代码中将 code-namespace 映射至 本地化资源:

```C#
services.Configure<AbpExceptionLocalizationOptions>(options =>
{
    options.MapCodeNamespace("Volo.Qa", typeof(QaResource));
});
```

然后`Volo.Qa`命名空间下的所有异常都将被对应的本地化资源进行本地化处理. 本地化资源中应包含对应错误代码的文本. 例如:

```json
{
  "culture": "en",
  "texts": {
    "Volo.Qa:010002": "You can not vote your own answer!"
  }
}
```

最后就可以抛出一个包含错误代码的业务异常了:

```C#
throw new BusinessException(QaDomainErrorCodes.CanNotVoteYourOwnAnswer);
```

- 抛出所有实现`IHasErrorCode` 接口的异常都具有相同的行为.因此,对错误代码的本地化,并不是`BusinessException`类所特有的.
- 为错误消息定义本地化文本并不是必须的. 如果未定义,ABP 会将默认的错误消息发送给客户端. 而不使用异常的`Message`属性. 如果你想要发送异常的`Message`,使用`UserFriendlyException`(或使用实现`IUserFriendlyException`接口的异常类型)

### 使用消息的格式化参数

如果有参数化的错误消息,则可以使用异常的`Data`属性进行设置.例如:

```C#
throw new BusinessException("App:010046")
{
    Data =
    {
        {"UserName", "john"}
    }
};

```

另外有一种更为快捷的方式:

```C#
throw new BusinessException("App:010046")
    .WithData("UserName", "john");
```

下面就是一个包含`UserName` 参数的错误消息:

```json
{
  "culture": "en",
  "texts": {
    "App:010046": "Username should be unique. '{UserName}' is already taken!"
  }
}
```

- `WithData` 支持有多个参数的链式调用 (如`.WithData(...).WithData(...)`).

## HTTP 状态代码映射

ABP 尝试按照以下规则,自动映射常见的异常类型的 HTTP 状态代码:

- 对于 `AbpAuthorizationException`:
  - 用户没有登录,返回 `401` (未认证).
  - 用户已登录,但是当前访问未授权,返回 `403` (未授权).
- 对于 `AbpValidationException` 返回 `400` (错误的请求) .
- 对于 `EntityNotFoundException`返回 `404` (未找到).
- 对于 `IBusinessException` 和 `IUserFriendlyException` (它是`IBusinessException`的扩展) 返回`403` (未授权) .
- 对于 `NotImplementedException` 返回 `501` (未实现) .
- 对于其他异常 (基础架构中未定义的) 返回 `500` (服务器内部错误) .

`IHttpExceptionStatusCodeFinder` 是用来自动判断 HTTP 状态代码.默认的实现是`DefaultHttpExceptionStatusCodeFinder`.可以根据需要对其进行更换或扩展.

#### 自定义映射

可以重写 HTTP 状态代码的自动映射,示例如下:

```C#
services.Configure<AbpExceptionHttpStatusCodeOptions>(options =>
{
    options.Map("Volo.Qa:010002", HttpStatusCode.Conflict);
});
```

## 发送异常详情到客户端

你可以通过 `AbpExceptionHandlingOptions` 类的 `SendExceptionsDetailsToClients` 属性异常发送到客户端:

```csharp
services.Configure<AbpExceptionHandlingOptions>(options =>
{
    options.SendExceptionsDetailsToClients = true;
});
```
