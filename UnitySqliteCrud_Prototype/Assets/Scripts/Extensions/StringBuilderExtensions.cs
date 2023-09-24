using System.Text;

public static class StringBuilderExtensions
{
    public static void AppendUser(this StringBuilder sb, User user)
    {
        sb.AppendLine("Id: " + user.Id);
        sb.AppendLine("Name: " + user.Name);
        sb.AppendLine("Email: " + user.Email);
        sb.AppendLine("CreatedDate: " + user.CreatedDate);
        sb.AppendLine("LastUpdatedDate: " + user.LastUpdatedDate);
    }
}