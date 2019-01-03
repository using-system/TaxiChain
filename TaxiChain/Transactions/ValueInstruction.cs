namespace TaxiChain.Transactions
{
    using System;
    using System.Collections.Generic;

    using NBlockchain.Models;

    /// <summary>
    /// Value Instruction base class
    /// </summary>
    /// <seealso cref="NBlockchain.Models.Instruction" />
    public abstract  class ValueInstruction : Instruction
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double Amount { get; set; }

        /// <summary>
        /// Extracts the signable elements.
        /// </summary>
        /// <returns></returns>
        public override ICollection<byte[]> ExtractSignableElements()
        {
            return new List<byte[]>() { BitConverter.GetBytes(Amount) };
        }
    }
}
