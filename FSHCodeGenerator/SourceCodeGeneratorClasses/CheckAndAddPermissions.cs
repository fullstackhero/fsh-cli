using System.Text;

namespace FSHCodeGenerator.SourceCodeGeneratorClasses;
internal class CheckAndAddPermissions
{
    public CheckAndAddPermissions(string pathtobasicsources, string pathtopermissions, Dictionary<string, string> allentities, string permissionsnamespace)
    {
        string endresource = string.Empty;
        string endpermissions = string.Empty;
        string FSHResourcetxt = string.Empty;
        string FSHPermissionstxt = string.Empty;
        string permissions = File.ReadAllText(pathtopermissions);

        foreach (var eKey in allentities)
        {

            bool canaddpermissions = permissions.IndexOf(eKey.Value) == -1;  // entity not found we must add....

            if (canaddpermissions)
            {

                FSHResourcetxt = FSHResourcetxt + "public const string " + eKey.Value + " = nameof(" + eKey.Value + ");" + Environment.NewLine + "\t";
                FSHPermissionstxt = FSHPermissionstxt + Environment.NewLine + "new(\"View " + eKey.Value + "\", FSHAction.View, FSHResource." + eKey.Value + ", IsBasic: true),";
                FSHPermissionstxt = FSHPermissionstxt + Environment.NewLine + "\t" + "\t" + "new(\"Search " + eKey.Value + "\", FSHAction.Search, FSHResource." + eKey.Value + ", IsBasic: true),";
                FSHPermissionstxt = FSHPermissionstxt + Environment.NewLine + "\t" + "\t" + "new(\"Create " + eKey.Value + "\", FSHAction.Create, FSHResource." + eKey.Value + "),";
                FSHPermissionstxt = FSHPermissionstxt + Environment.NewLine + "\t" + "\t" + "new(\"Update " + eKey.Value + "\",FSHAction.Update, FSHResource." + eKey.Value + "),";
                FSHPermissionstxt = FSHPermissionstxt + Environment.NewLine + "\t" + "\t" + "new(\"Delete " + eKey.Value + "\",FSHAction.Delete, FSHResource." + eKey.Value + "),";
                FSHPermissionstxt = FSHPermissionstxt + Environment.NewLine + "\t" + "\t" + "new(\"Generate " + eKey.Value + "\", FSHAction.Generate, FSHResource." + eKey.Value + "),";
                FSHPermissionstxt = FSHPermissionstxt + Environment.NewLine + "\t" + "\t" + "new(\"Clean " + eKey.Value + "\",FSHAction.Clean, FSHResource." + eKey.Value + "),";

            }
        }

        if (!String.IsNullOrEmpty(FSHPermissionstxt))
            {
            string newpermissions = File.ReadAllText(pathtobasicsources + "AddPermissions.txt")
                       .Replace("<&NameSpace&>", permissionsnamespace)
                       .Replace("<&FSHResource&>", FSHResourcetxt)
                       .Replace("<&FSHPermissions&>", FSHPermissionstxt);
            
            File.WriteAllText(pathtopermissions, newpermissions);
        }
    }
}
