using MudBlazor;

namespace Blackshield.Theme;

public static class BlackshieldTheme
{
    // mikasi – warmes, helles Papier-Theme mit Salbeigrün-Akzent (siehe LandingPageVorlage)
    private static readonly PaletteLight LightPalette = new()
    {
        Primary               = "#2F6B4F",
        PrimaryDarken         = "#275A42",
        PrimaryLighten        = "#5E9079",
        PrimaryContrastText   = "#FFFFFF",
        Secondary             = "#C2853E",
        SecondaryDarken       = "#A86E2C",
        SecondaryLighten      = "#E0B981",
        Tertiary              = "#5A5F5A",
        Info                  = "#4E879A",
        Success               = "#3B8B5E",
        Warning               = "#C2853E",
        Error                 = "#C24A38",
        Dark                  = "#1A1D1A",
        Black                 = "#11130F",
        White                 = "#FFFFFF",

        Background            = "#FAF8F4",
        BackgroundGray        = "#F2EDE3",
        Surface               = "#FFFFFF",
        DrawerBackground      = "#FFFFFF",
        DrawerText            = "#5A5F5A",
        DrawerIcon            = "#6B716A",
        AppbarBackground      = "#FFFFFF",
        AppbarText            = "#1A1D1A",

        TextPrimary           = "#1A1D1A",
        TextSecondary         = "#5A5F5A",
        TextDisabled          = "#A8A59C",
        ActionDefault         = "#5A5F5A",
        ActionDisabled        = "#CFCABF",
        ActionDisabledBackground = "#F2EDE3",

        LinesDefault          = "#E2DDD2",
        LinesInputs           = "#D6D0C4",
        TableLines            = "#E2DDD2",
        TableStriped          = "#FAF8F4",
        TableHover            = "#F2EDE3",
        Divider               = "#E2DDD2",
        DividerLight          = "#F2EDE3"
    };

    // Dunkler Gegenpart: warmes Grün auf tiefem Tann-/Tinten-Ton
    private static readonly PaletteDark DarkPalette = new()
    {
        Primary               = "#7FB79A",
        PrimaryDarken         = "#5E9079",
        PrimaryLighten        = "#A8D0BC",
        PrimaryContrastText   = "#11130F",
        Secondary             = "#D9A883",
        SecondaryDarken       = "#C2853E",
        SecondaryLighten      = "#EAC9A8",
        Tertiary              = "#9AA39A",
        Info                  = "#7FB0C0",
        Success               = "#6FB98C",
        Warning               = "#E0B981",
        Error                 = "#E08577",
        Dark                  = "#0F110D",
        Black                 = "#000000",
        White                 = "#FFFFFF",

        Background            = "#1A1D18",
        BackgroundGray        = "#12140F",
        Surface               = "#24271F",
        DrawerBackground      = "#141711",
        DrawerText            = "#C9C6BC",
        DrawerIcon            = "#9AA39A",
        AppbarBackground      = "#141711",
        AppbarText            = "#F2EDE3",

        TextPrimary           = "#F2EDE3",
        TextSecondary         = "#A8A59C",
        TextDisabled          = "#6B716A",
        ActionDefault         = "#A8A59C",
        ActionDisabled        = "#3A3E34",
        ActionDisabledBackground = "#24271F",

        LinesDefault          = "#363A30",
        LinesInputs           = "#4A4F44",
        TableLines            = "#363A30",
        TableStriped          = "#1F2219",
        TableHover            = "#2A2E24",
        Divider               = "#363A30",
        DividerLight          = "#24271F"
    };

    public static readonly MudTheme Instance = new()
    {
        PaletteLight = LightPalette,
        PaletteDark  = DarkPalette,
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "14px",
            DrawerWidthLeft     = "260px",
            AppbarHeight        = "64px"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Outfit", "system-ui", "-apple-system", "sans-serif"],
                FontSize   = "0.9375rem",
                FontWeight = "400",
                LineHeight = "1.6",
                LetterSpacing = "0"
            },
            // Display-Überschriften in der Serifenschrift Fraunces (wie Logo & Section-Heads der Vorlage)
            H1 = new H1Typography
            {
                FontFamily = ["Fraunces", "Georgia", "serif"],
                FontWeight = "600",
                FontSize   = "2.6rem",
                LineHeight = "1.1",
                LetterSpacing = "-0.02em"
            },
            H2 = new H2Typography
            {
                FontFamily = ["Fraunces", "Georgia", "serif"],
                FontWeight = "600",
                FontSize   = "2rem",
                LineHeight = "1.18",
                LetterSpacing = "-0.018em"
            },
            H3 = new H3Typography
            {
                FontFamily = ["Fraunces", "Georgia", "serif"],
                FontWeight = "600",
                FontSize   = "1.625rem",
                LineHeight = "1.22",
                LetterSpacing = "-0.015em"
            },
            H4 = new H4Typography
            {
                FontFamily = ["Fraunces", "Georgia", "serif"],
                FontWeight = "600",
                FontSize   = "1.4rem",
                LineHeight = "1.28",
                LetterSpacing = "-0.012em"
            },
            // Kleinere Überschriften / Kartentitel bleiben in der Sans-Schrift Outfit
            H5 = new H5Typography
            {
                FontFamily = ["Outfit", "system-ui", "sans-serif"],
                FontWeight = "600",
                FontSize   = "1.125rem",
                LineHeight = "1.4",
                LetterSpacing = "-0.01em"
            },
            H6 = new H6Typography
            {
                FontFamily = ["Outfit", "system-ui", "sans-serif"],
                FontWeight = "600",
                FontSize   = "1rem",
                LineHeight = "1.45",
                LetterSpacing = "-0.005em"
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
