// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace DocumentFormat.OpenXml.Framework
{
    internal static class ReservedElementTypeIds
    {
        /// <summary>
        /// Test whether the element is a strong typed element - the class is generated by CodeGen.
        /// </summary>
        /// <param name="element">The specified element.</param>
        /// <returns>True if the class of the element is generated.</returns>
        internal static bool IsStrongTypedElement(this OpenXmlElement element) => !element.IsReservedElement();

        internal static bool IsReservedElement(this OpenXmlElement element)
        {
            return element.IsAlternateContent() || element.IsAlternateContentChoice() || element.IsAlternateContentFallback() || element.IsUnknown() || element.IsMiscNode();
        }

        public static bool IsAlternateContent(this OpenXmlElement element) => element is AlternateContent;

        public static bool IsAlternateContentChoice(this OpenXmlElement element) => element is AlternateContentChoice;

        public static bool IsAlternateContentFallback(this OpenXmlElement element) => element is AlternateContentFallback;

        public static bool IsUnknown(this OpenXmlElement element) => element is OpenXmlUnknownElement;

        public static bool IsMiscNode(this OpenXmlElement element) => element is OpenXmlMiscNode;
    }
}
