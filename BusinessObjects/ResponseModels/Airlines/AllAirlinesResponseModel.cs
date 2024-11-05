namespace BusinessObjects.ResponseModels.Airlines
{
    public class AllAirlinesResponseModel
    {
        public string Id { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public bool Status { get; set; }

    }
}
