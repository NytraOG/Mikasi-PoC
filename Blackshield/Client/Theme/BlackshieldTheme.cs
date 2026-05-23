using MudBlazor;

namespace Blackshield.Theme;

public static class BlackshieldTheme
{
    private static readonly PaletteLight LightPalette = new()
    {
        Primary               = "#4F46E5",
        PrimaryDarken         = "#4338CA",
        PrimaryLighten        = "#A5B4FC",
        PrimaryContrastText   = "#FFFFFF",
        Secondary             = "#F59E0B",
        SecondaryDarken       = "#D97706",
        SecondaryLighten      = "#FCD34D",
        Tertiary              = "#10B981",
        Info                  = "#0EA5E9",
        Success               = "#10B981",
        Warning               = "#F59E0B",
        Error                 = "#EF4444",
        Dark                  = "#0F172A",
        Black                 = "#0B1120",
        White                 = "#FFFFFF",

        Background            = "#F7F8FB",
        BackgroundGray        = "#EEF0F5",
        Surface               = "#FFFFFF",
        DrawerBackground      = "#FFFFFF",
        DrawerText            = "#475569",
        DrawerIcon            = "#64748B",
        AppbarBackground      = "#FFFFFF",
        AppbarText            = "#0F172A",

        TextPrimary           = "#0F172A",
        TextSecondary         = "#475569",
        TextDisabled          = "#94A3B8",
        ActionDefault         = "#475569",
        ActionDisabled        = "#CBD5E1",
        ActionDisabledBackground = "#F1F5F9",

        LinesDefault          = "#E2E8F0",
        LinesInputs           = "#CBD5E1",
        TableLines            = "#E2E8F0",
        TableStriped          = "#F8FAFC",
        TableHover            = "#F1F5F9",
        Divider               = "#E2E8F0",
        DividerLight          = "#F1F5F9"
    };

    private static readonly PaletteDark DarkPalette = new()
    {
        Primary               = "#818CF8",
        PrimaryDarken         = "#6366F1",
        PrimaryLighten        = "#C7D2FE",
        PrimaryContrastText   = "#0F172A",
        Secondary             = "#FBBF24",
        SecondaryDarken       = "#F59E0B",
        SecondaryLighten      = "#FDE68A",
        Tertiary              = "#34D399",
        Info                  = "#38BDF8",
        Success               = "#34D399",
        Warning               = "#FBBF24",
        Error                 = "#F87171",
        Dark                  = "#020617",
        Black                 = "#000000",
        White                 = "#FFFFFF",

        Background            = "#0F172A",
        BackgroundGray        = "#0B1220",
        Surface               = "#1E293B",
        DrawerBackground      = "#020617",
        DrawerText            = "#CBD5E1",
        DrawerIcon            = "#94A3B8",
        AppbarBackground      = "#020617",
        AppbarText            = "#F1F5F9",

        TextPrimary           = "#F1F5F9",
        TextSecondary         = "#94A3B8",
        TextDisabled          = "#475569",
        ActionDefault         = "#94A3B8",
        ActionDisabled        = "#334155",
        ActionDisabledBackground = "#1E293B",

        LinesDefault          = "#334155",
        LinesInputs           = "#475569",
        TableLines            = "#334155",
        TableStriped          = "#162032",
        TableHover            = "#243049",
        Divider               = "#334155",
        DividerLight          = "#1E293B"
    };

    public static readonly MudTheme Instance = new()
    {
        PaletteLight = LightPalette,
        PaletteDark  = DarkPalette,
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "12px",
            DrawerWidthLeft     = "260px",
            AppbarHeight        = "64px"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Inter", "system-ui", "-apple-system", "sans-serif"],
                FontSize   = "0.9375rem",
                FontWeight = "400",
                LineHeight = "1.55",
                LetterSpacing = "-0.005em"
            },
            H1 = new H1Typography
            {
                FontFamily = ["Plus Jakarta Sans", "Inter", "sans-serif"],
                FontWeight = "800",
                FontSize   = "2.5rem",
                LineHeight = "1.15",
                LetterSpacing = "-0.025em"
            },
            H2 = new H2Typography
            {
                FontFamily = ["Plus Jakarta Sans", "Inter", "sans-serif"],
                FontWeight = "700",
                FontSize   = "2rem",
                LineHeight = "1.2",
                LetterSpacing = "-0.02em"
            },
            H3 = new H3Typography
            {
                FontFamily = ["Plus Jakarta Sans", "Inter", "sans-serif"],
                FontWeight = "700",
                FontSize   = "1.625rem",
                LineHeight = "1.25",
                LetterSpacing = "-0.018em"
            },
            H4 = new H4Typography
            {
                FontFamily = ["Plus Jakarta Sans", "Inter", "sans-serif"],
                FontWeight = "700",
                FontSize   = "1.375rem",
                LineHeight = "1.3",
                LetterSpacing = "-0.015em"
            },
            H5 = new H5Typography
            {
                FontFamily = ["Plus Jakarta Sans", "Inter", "sans-serif"],
                FontWeight = "600",
                FontSize   = "1.125rem",
                LineHeight = "1.4"
            },
            H6 = new H6Typography
            {
                FontFamily = ["Plus Jakarta Sans", "Inter", "sans-serif"],
                FontWeight = "600",
                FontSize   = "1rem",
                LineHeight = "1.45"
            },
            Subtitle1 = new Subtitle1Typography
            {
                FontWeight = "500",
                FontSize   = "0.9375rem"
            },
            Subtitle2 = new Subtitle2Typography
            {
                FontWeight = "500",
                FontSize   = "0.8125rem"
            },
            Button = new ButtonTypography
            {
                FontWeight    = "600",
                FontSize      = "0.875rem",
                LetterSpacing = "0",
                TextTransform = "none"
            },
            Caption = new CaptionTypography
            {
                FontSize = "0.75rem"
            }
        }
    };
}
