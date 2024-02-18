namespace Core.Constants;

public static class ConfigurationsConstants
{
    public static class Document
    {
        public const int NameMaxLength = 512;
        public const int NameMinLength = 1;

        public const int DirectoryMaxLength = 512;
        public const int DirectoryMinLength = 1;

        public const int PriorityMaxLength = 16;
    }

    public static class UploadedFile
    {
        public const int FileNameMaxLength = 1024;
        public const int FileNameMinLength = 1;

        public const int FakeNameMaxLength = 512;

        public const int ContentTypeMaxLength = 32;
    }

    public static class AllowedExtension
    {
        public const int ExtensionMaxLength = 32;
        public const int ExtensionMinLength = 1;
    }
}
