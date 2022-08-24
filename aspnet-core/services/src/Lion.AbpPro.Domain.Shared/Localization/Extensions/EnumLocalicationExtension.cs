// namespace Lion.AbpPro.Localization.Extensions
// {
//     public static class EnumLocalicationExtension
//     {
//         public static string ToLocalicationDescription(this Enum value)
//         {
//             var member =
//                 ((IEnumerable<MemberInfo>)value.GetType().GetMember(value.ToString()))
//                 .FirstOrDefault<MemberInfo>();
//
//             var localKey =$"Enum:{member.ReflectedType.Name}:{value}:{Convert.ToInt16(value)}";
//             if (localKey.IsNullOrWhiteSpace())
//             {
//                 throw new ArgumentException();
//             }
//             return !(member != (MemberInfo)null) ? value.ToString() : LocalizationHelper.L[localKey];
//         }
//     }
// }