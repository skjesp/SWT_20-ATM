﻿namespace SWT_20_ATM
{
    public interface ILogger
    {
        /// <summary>
        /// Writes a message to a logger
        /// </summary>
        /// <param name="message"></param>
        void AddToLog( string message );
    }
}