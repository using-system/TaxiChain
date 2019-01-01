namespace TaxiChain.Transactions
{
    using System;
    using System.Collections.Generic;

    using NBlockchain.Models;

    using TaxiChain.Model;

    /// <summary>
    /// Request Driver Instruction
    /// </summary>
    /// <seealso cref="NBlockchain.Models.Instruction" />
    public class RequestDriverInstruction : Instruction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestDriverInstruction"/> class.
        /// </summary>
        public RequestDriverInstruction()
        {
            this.Start = new Position();
        }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public Position Start { get; set; }

        public override ICollection<byte[]> ExtractSignableElements()
        {
            return new List<byte[]>()
            {
                BitConverter.GetBytes(this.Start.Latitude),
                BitConverter.GetBytes(this.Start.Longitude)
            };
        }
    }
}
