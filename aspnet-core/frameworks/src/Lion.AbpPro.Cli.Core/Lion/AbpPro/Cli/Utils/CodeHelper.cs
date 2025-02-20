using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Lion.AbpPro.Cli.Utils;

public static class CodeHelper
{
    /// <summary>
    /// 把指定代码添加到对应方法中
    /// </summary>
    public static void AddCodeToMethod(string filePath, string methodName, string addCode)
    {
        try
        {
            // 读取文件内容
            var code = File.ReadAllText(filePath);

            // 解析代码为语法树
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetCompilationUnitRoot();

            // 查找所有的方法声明
            var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            // 筛选出名为 ConfigureOA 的方法
            var configureOAMethod = methodDeclarations.FirstOrDefault(m => m.Identifier.ValueText == methodName);

            if (configureOAMethod == null) return;
            // 解析要添加的代码为语句
            var newStatement = CSharpSyntaxTree.ParseText(addCode).GetCompilationUnitRoot().DescendantNodes().OfType<StatementSyntax>().FirstOrDefault();

            if (newStatement == null) return;
            // 在方法体中添加新语句
            var newMethodBody = configureOAMethod?.Body?.AddStatements(newStatement);
            var newMethod = configureOAMethod.WithBody(newMethodBody);

            // 替换原方法为新方法
            var newRoot = root.ReplaceNode(configureOAMethod, newMethod);

            // 获取更新后的代码文本
            var newCode = newRoot.GetText().ToString();
            // 将更新后的代码写回文件
            File.WriteAllText(filePath, newCode);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"发生错误AddCodeToMethod: {ex.Message}");
        }
    }

    public static void AddCodeToClass(string filePath, string className, string addCode)
    {
        try
        {
            // 读取文件内容
            var code = File.ReadAllText(filePath);
            // 解析代码为语法树
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetCompilationUnitRoot();

            // 查找 IOADbContext 接口声明
            var interfaceDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>()
                .FirstOrDefault(i => i.Identifier.ValueText == className);

            if (interfaceDeclaration == null) return;
            // 解析要添加的属性代码
            var newProperty = CSharpSyntaxTree.ParseText(addCode).GetCompilationUnitRoot()
                .DescendantNodes().OfType<PropertyDeclarationSyntax>().FirstOrDefault();

            if (newProperty == null) return;
            // 在接口成员中添加新属性
            var newMembers = interfaceDeclaration.Members.Add(newProperty);
            var newInterface = interfaceDeclaration.WithMembers(newMembers);

            // 替换原接口声明为新接口声明
            var newRoot = root.ReplaceNode(interfaceDeclaration, newInterface);

            // 获取更新后的代码文本
            var newCode = newRoot.GetText().ToString();
            // 将更新后的代码写回文件
            File.WriteAllText(filePath, newCode);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"发生错误AddCodeToClass: {ex.Message}");
        }
    }

    public static void AddCodeToInterface(string filePath, string interfaceName, string addCode)
    {
        try
        {
            // 读取文件内容
            var code = File.ReadAllText(filePath);

            // 解析代码为语法树
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetCompilationUnitRoot();

            // 查找 IOADbContext 接口声明
            var interfaceDeclaration = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>()
                .FirstOrDefault(i => i.Identifier.ValueText == interfaceName);

            if (interfaceDeclaration == null) return;
            // 解析要添加的属性代码
            var newProperty = CSharpSyntaxTree.ParseText(addCode).GetCompilationUnitRoot()
                .DescendantNodes().OfType<PropertyDeclarationSyntax>().FirstOrDefault();

            if (newProperty == null) return;
            // 在接口成员中添加新属性
            var newMembers = interfaceDeclaration.Members.Add(newProperty);
            var newInterface = interfaceDeclaration.WithMembers(newMembers);

            // 替换原接口声明为新接口声明
            var newRoot = root.ReplaceNode(interfaceDeclaration, newInterface);

            // 获取更新后的代码文本
            var newCode = newRoot.GetText().ToString();
            // 将更新后的代码写回文件
            File.WriteAllText(filePath, newCode);
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"发生错误AddCodeToInterface: {ex.Message}");
        }
    }

    public static void AddUsing(string filePath, string addCode)
    {
        try
        {
            // 读取文件内容
            var code = File.ReadAllText(filePath);

            // 在第一行添加代码
            var newSourceText = SourceText.From(addCode + Environment.NewLine + code);

            // 将修改后的内容写回文件
            File.WriteAllText(filePath, newSourceText.ToString());
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"发生错误AddUsing: {ex.Message}");
        }
    }

    public static bool IsExistCode(string filePath, string code)
    {
        var content = File.ReadAllText(filePath).Replace(" ", "");
        return content.Contains(code.Replace(" ","").Replace("\r\n",""));
    }
}
