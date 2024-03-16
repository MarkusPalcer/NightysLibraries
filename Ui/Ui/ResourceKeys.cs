namespace Ui;

public static class ResourceKeys
{
    private static readonly string Prefix = typeof(ResourceKeys).FullName!;

    public static readonly string RightArrow = string.Join(".", Prefix, nameof(RightArrow));
    public static readonly string Checkmark = string.Join(".", Prefix, nameof(Checkmark));

    public static class Enabled
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        private static readonly string Prefix = typeof(Enabled).FullName!;

        public static readonly string Foreground = string.Join(".", Prefix, nameof(Foreground));
        public static readonly string Background = string.Join(".", Prefix, nameof(Background));
    }

    public static class Disabled
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        private static readonly string Prefix = typeof(Disabled).FullName!;

        public static readonly string Foreground = string.Join(".", Prefix, nameof(Foreground));
        public static readonly string Background = string.Join(".", Prefix, nameof(Background));
    }

    public static class Highlight
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        private static readonly string Prefix = typeof(Highlight).FullName!;

        public static readonly string Border = string.Join(".", Prefix, nameof(Border));
        public static readonly string Overlay = string.Join(".", Prefix, nameof(Overlay));
        public static readonly string Background = string.Join(".", Prefix, nameof(Background));
    }

    public static class Focus
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        private static readonly string Prefix = typeof(Focus).FullName!;

        public static readonly string Overlay = string.Join(".", Prefix, nameof(Overlay));
    }

    public static class Menu
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        private static readonly string Prefix = typeof(Menu).FullName!;

        public static readonly string SubmenuCheckmarkStyle = string.Join(".", Prefix, nameof(SubmenuCheckmarkStyle));
        public static readonly string CheckMarkStyle = string.Join(".", Prefix, nameof(CheckMarkStyle));

        public static readonly string IconStyle = string.Join(".", Prefix, nameof(IconStyle));
        public static readonly string SubmenuBorderStyle = string.Join(".", Prefix, nameof(SubmenuBorderStyle));
        public static readonly string TopLevelCheckmarkStyle = string.Join(".", Prefix, nameof(TopLevelCheckmarkStyle));
        public static readonly string SubmenuCheckmarkPanelStyle = string.Join(".", Prefix, nameof(SubmenuCheckmarkPanelStyle));

        public static readonly string SubmenuItemIconColumnGroupMinWidth = string.Join(".", Prefix, nameof(SubmenuItemIconColumnGroupMinWidth));
    }
}