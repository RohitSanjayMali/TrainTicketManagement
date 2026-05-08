namespace TrainTicketManagement.Helpers
{
    public static class PnrGenerator
    {
        // FIXED: Guid-based — unique and non-guessable
        // Old: TR + 6-digit random (easy to guess, possible duplicates)
        // New: TR + 8 random hex chars e.g. TR4A2B9C1D
        public static string GeneratePNR()
        {
            return "TR" + Guid.NewGuid().ToString("N")[..8].ToUpper();
        }
    }
}
