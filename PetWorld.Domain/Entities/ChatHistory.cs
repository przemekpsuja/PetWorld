namespace PetWorld.Domain.Entities
{
    /// <summary>
    /// Represents a chat history in the PetWorld store.
    /// </summary>
    public class ChatHistory
    {
        /// <summary>
        /// Gets or sets the unique identifier of the message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the message.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the question send to the chat.
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the answer send by AI.
        /// </summary>
        public string Answer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of iteration.
        /// </summary>
        public int IterationCount { get; set; }
    }
}
