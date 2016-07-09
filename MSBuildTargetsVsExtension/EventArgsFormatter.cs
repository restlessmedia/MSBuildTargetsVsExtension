﻿using System;
using System.Globalization;
using System.Text;
using Microsoft.Build.Framework;

namespace MSBuildTargetsVsExtension
{
    internal static class EventArgsFormatter
    {
        /// <summary>
        /// Escape the carriage Return from a string
        /// </summary>
        /// <param name="stringWithCarriageReturn"></param>
        /// <returns>String with carriage returns escaped as \\r </returns>
        internal static string EscapeCarriageReturn(string stringWithCarriageReturn)
        {
            if (!string.IsNullOrEmpty(stringWithCarriageReturn))
                return stringWithCarriageReturn.Replace("\r", "\\r");

            // If the string is null or empty or then we just return the string
            return stringWithCarriageReturn;
        }

        /// <summary>
        /// Format the error event message and all the other event data into
        /// a single string.
        /// </summary>
        /// <param name="e">Error to format</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildErrorEventArgs e)
        {
            return FormatEventMessage(e, false);
        }

        /// <summary>
        /// Format the error event message and all the other event data into
        /// a single string.
        /// </summary>
        /// <param name="e">Error to format</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildErrorEventArgs e, bool removeCarriageReturn)
        {
            return FormatEventMessage("error", e.Subcategory, removeCarriageReturn ? EscapeCarriageReturn(e.Message) : e.Message,
                            e.Code, e.File, null, e.LineNumber, e.EndLineNumber,
                            e.ColumnNumber, e.EndColumnNumber, e.ThreadId);
        }

        /// <summary>
        /// Format the error event message and all the other event data into
        /// a single string.
        /// </summary>
        /// <param name="e">Error to format</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildErrorEventArgs e, bool removeCarriageReturn, bool showProjectFile)
        {
            return FormatEventMessage("error", e.Subcategory, removeCarriageReturn ? EscapeCarriageReturn(e.Message) : e.Message,
                e.Code, e.File, showProjectFile ? e.ProjectFile : null, e.LineNumber, e.EndLineNumber,
                            e.ColumnNumber, e.EndColumnNumber, e.ThreadId);
        }

        /// <summary>
        /// Format the warning message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="e">Warning to format</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildWarningEventArgs e)
        {
            return FormatEventMessage(e, false);
        }

        /// <summary>
        /// Format the warning message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="e">Warning to format</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildWarningEventArgs e, bool removeCarriageReturn)
        {
            return FormatEventMessage("warning", e.Subcategory, removeCarriageReturn ? EscapeCarriageReturn(e.Message) : e.Message,
                e.Code, e.File, null, e.LineNumber, e.EndLineNumber,
                           e.ColumnNumber, e.EndColumnNumber, e.ThreadId);
        }

        /// <summary>
        /// Format the warning message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="e">Warning to format</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildWarningEventArgs e, bool removeCarriageReturn, bool showProjectFile)
        {
            return FormatEventMessage("warning", e.Subcategory, removeCarriageReturn ? EscapeCarriageReturn(e.Message) : e.Message,
                e.Code, e.File, showProjectFile ? e.ProjectFile : null, e.LineNumber, e.EndLineNumber,
                           e.ColumnNumber, e.EndColumnNumber, e.ThreadId);
        }

        /// <summary>
        /// Format the message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="e">Message to format</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildMessageEventArgs e)
        {
            return FormatEventMessage(e, false);
        }

        /// <summary>
        /// Format the message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="e">Message to format</param>
        /// <param name="removeCarriageReturn">Escape CR or leave as is</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildMessageEventArgs e, bool removeCarriageReturn)
        {
            return FormatEventMessage(e, removeCarriageReturn, false);
        }

        /// <summary>
        /// Format the message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="e">Message to format</param>
        /// <param name="removeCarriageReturn">Escape CR or leave as is</param>
        /// <param name="showProjectFile">Show project file or not</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage(BuildMessageEventArgs e, bool removeCarriageReturn, bool showProjectFile)
        {
            return FormatEventMessage("message", e.Subcategory, removeCarriageReturn ? EscapeCarriageReturn(e.Message) : e.Message,
                e.Code, e.File, showProjectFile ? e.ProjectFile : null, e.LineNumber, e.EndLineNumber, e.ColumnNumber, e.EndColumnNumber, e.ThreadId);
        }

        /// <summary>
        /// Format the event message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="category">category ("error" or "warning")</param>
        /// <param name="subcategory">subcategory</param>
        /// <param name="message">event message</param>
        /// <param name="code">error or warning code number</param>
        /// <param name="file">file name</param>
        /// <param name="lineNumber">line number (0 if n/a)</param>
        /// <param name="endLineNumber">end line number (0 if n/a)</param>
        /// <param name="columnNumber">column number (0 if n/a)</param>
        /// <param name="endColumnNumber">end column number (0 if n/a)</param>
        /// <param name="threadId">thread id</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage
        (
            string category,
            string subcategory,
            string message,
            string code,
            string file,
            int lineNumber,
            int endLineNumber,
            int columnNumber,
            int endColumnNumber,
            int threadId
        )
        {
            return FormatEventMessage(category, subcategory, message, code, file, null, lineNumber, endLineNumber, columnNumber, endColumnNumber, threadId);
        }

        /// <summary>
        /// Format the event message and all the other event data into a
        /// single string.
        /// </summary>
        /// <param name="category">category ("error" or "warning")</param>
        /// <param name="subcategory">subcategory</param>
        /// <param name="message">event message</param>
        /// <param name="code">error or warning code number</param>
        /// <param name="file">file name</param>
        /// <param name="projectFile">the project file name</param>
        /// <param name="lineNumber">line number (0 if n/a)</param>
        /// <param name="endLineNumber">end line number (0 if n/a)</param>
        /// <param name="columnNumber">column number (0 if n/a)</param>
        /// <param name="endColumnNumber">end column number (0 if n/a)</param>
        /// <param name="threadId">thread id</param>
        /// <returns>The formatted message string.</returns>
        internal static string FormatEventMessage
        (
            string category,
            string subcategory,
            string message,
            string code,
            string file,
            string projectFile,
            int lineNumber,
            int endLineNumber,
            int columnNumber,
            int endColumnNumber,
            int threadId
        )
        {
            var format = new StringBuilder();

            // Uncomment these lines to show show the processor, if present.
            /*
            if (threadId != 0)
            {
                format.Append("{0}>");
            }
            */

            if ((file == null) || (file.Length == 0))
            {
                format.Append("MSBUILD : ");    // Should not be localized.
            }
            else
            {
                format.Append("{1}");

                if (lineNumber == 0)
                {
                    format.Append(" : ");
                }
                else
                {
                    if (columnNumber == 0)
                    {
                        if (endLineNumber == 0)
                        {
                            format.Append("({2}): ");
                        }
                        else
                        {
                            format.Append("({2}-{7}): ");
                        }
                    }
                    else
                    {
                        if (endLineNumber == 0)
                        {
                            if (endColumnNumber == 0)
                            {
                                format.Append("({2},{3}): ");
                            }
                            else
                            {
                                format.Append("({2},{3}-{8}): ");
                            }
                        }
                        else
                        {
                            if (endColumnNumber == 0)
                            {
                                format.Append("({2}-{7},{3}): ");
                            }
                            else
                            {
                                format.Append("({2},{3},{7},{8}): ");
                            }
                        }
                    }
                }
            }

            if ((subcategory != null) && (subcategory.Length != 0))
            {
                format.Append("{9} ");
            }

            // The category as a string (should not be localized)
            format.Append("{4} ");

            // Put a code in, if available and necessary.
            if (code == null)
            {
                format.Append(": ");
            }
            else
            {
                format.Append("{5}: ");
            }

            // Put the message in, if available.
            if (message != null)
            {
                format.Append("{6}");
            }

            // If the project file was specified, tack that onto the very end.
            if (projectFile != null && !String.Equals(projectFile, file))
            {
                format.Append(" [{10}]");
            }

            // A null message is allowed and is to be treated as a blank line.
            if (null == message)
            {
                message = String.Empty;
            }

            var finalFormat = format.ToString();

            // If there are multiple lines, show each line as a separate message.
            var lines = SplitStringOnNewLines(message);
            var formattedMessage = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                formattedMessage.Append(String.Format(
                        CultureInfo.CurrentCulture, finalFormat,
                        threadId, file,
                        lineNumber, columnNumber, category, code,
                        lines[i], endLineNumber, endColumnNumber,
                        subcategory, projectFile));

                if (i < (lines.Length - 1))
                {
                    formattedMessage.AppendLine();
                }
            }

            return formattedMessage.ToString();
        }


        /// <summary>
        /// Splits strings on 'newLines' with tolerance for Everett and Dogfood builds.
        /// </summary>
        /// <param name="s">String to split.</param>
        private static string[] SplitStringOnNewLines(string s)
        {
            var subStrings = s.Split(s_newLines, StringSplitOptions.None);
            return subStrings;
        }

        /// <summary>
        /// The kinds of newline breaks we expect.
        /// </summary>
        /// <remarks>Currently we're not supporting "\r".</remarks>
        private static readonly string[] s_newLines = { "\r\n", "\n" };
    }
}
