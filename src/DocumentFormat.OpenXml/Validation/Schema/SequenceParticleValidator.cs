// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace DocumentFormat.OpenXml.Validation.Schema
{
    /// <summary>
    /// Sequence particle validator.
    /// </summary>
    internal class SequenceParticleValidator : CompositeParticleValidator
    {
        /// <summary>
        /// Initializes a new instance of the SequenceParticleValidator.
        /// </summary>
        /// <param name="particleConstraint"></param>
        internal SequenceParticleValidator(CompositeParticle particleConstraint)
            : base(particleConstraint)
        {
            Debug.Assert(particleConstraint.ParticleType == ParticleType.Sequence);
        }

        /// <summary>
        /// Try match the particle once.
        /// </summary>
        /// <param name="particleMatchInfo"></param>
        /// <param name="validationContext">The context information for validation.</param>
        public override void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
        {
            Debug.Assert(!(particleMatchInfo.StartElement is OpenXmlMiscNode));

            var next = particleMatchInfo.StartElement;

            particleMatchInfo.LastMatchedElement = null;
            particleMatchInfo.Match = ParticleMatch.Nomatch;

            ParticleConstraint childConstraint;
            int constraintIndex = 0;
            int constraintTotal = ParticleConstraint.ChildrenParticles.Length;

            var childMatchInfo = new ParticleMatchInfo();

            while (constraintIndex < constraintTotal && next is not null)
            {
                childConstraint = ParticleConstraint.ChildrenParticles[constraintIndex];

                if (childConstraint.ParticleValidator is null)
                {
                    constraintIndex++;
                    continue;
                }

                // Use Reset() instead of new() to avoid heavy memory allocation and GC.
                childMatchInfo.Reset(next);

                childConstraint.ParticleValidator.TryMatch(childMatchInfo, validationContext);

                // if the _childMatchInfo.StartElement is changed, it means this method of this object is called more than once on the stack.
                Debug.Assert(childMatchInfo.StartElement == next);

                switch (childMatchInfo.Match)
                {
                    case ParticleMatch.Nomatch:
                        if (childConstraint.ParticleValidator.GetRequiredElements(null))
                        {
                            if (validationContext.CollectExpectedChildren)
                            {
                                if (particleMatchInfo.ExpectedChildren is null)
                                {
                                    particleMatchInfo.SetExpectedChildren(childConstraint.ParticleValidator.GetRequiredElements());
                                }
                                else
                                {
                                    // reuse same object, avoid object allocation.
                                    particleMatchInfo.ExpectedChildren.Clear();
                                    childConstraint.ParticleValidator.GetRequiredElements(particleMatchInfo.ExpectedChildren);
                                }
                            }

                            // incomplete children.
                            if (next == particleMatchInfo.StartElement)
                            {
                                // the first child is not the correct one.
                                particleMatchInfo.Match = ParticleMatch.Nomatch;
                                particleMatchInfo.LastMatchedElement = null;
                                return;
                            }
                            else
                            {
                                // partial match, incomplete children.
                                particleMatchInfo.Match = ParticleMatch.Partial;
                                return;
                            }
                        }
                        else
                        {
                            // continue trying match next child constraint.
                            constraintIndex++;
                            continue;
                        }

                    case ParticleMatch.Matched:
                        particleMatchInfo.LastMatchedElement = childMatchInfo.LastMatchedElement;
                        next = validationContext.GetNextChildMc(particleMatchInfo.LastMatchedElement);

                        // continue trying match next child constraint.
                        constraintIndex++;
                        break;

                    case ParticleMatch.Partial:
                        // partial match, incomplete children.
                        particleMatchInfo.Match = ParticleMatch.Partial;
                        particleMatchInfo.LastMatchedElement = childMatchInfo.LastMatchedElement;
                        if (validationContext.CollectExpectedChildren)
                        {
                            particleMatchInfo.SetExpectedChildren(childMatchInfo.ExpectedChildren);
                        }

                        return;
                }
            }

            if (constraintIndex == constraintTotal)
            {
                if (particleMatchInfo.LastMatchedElement is not null)
                {
                    particleMatchInfo.Match = ParticleMatch.Matched;
                }
                else
                {
                    particleMatchInfo.Match = ParticleMatch.Nomatch;
                }

                return;
            }
            else
            {
                for (; constraintIndex < constraintTotal; constraintIndex++)
                {
                    var validator = ParticleConstraint.ChildrenParticles[constraintIndex].ParticleValidator;

                    if (validator is null)
                    {
                        continue;
                    }

                    if (validator.GetRequiredElements(null))
                    {
                        if (validationContext.CollectExpectedChildren)
                        {
                            validator.GetRequiredElements(particleMatchInfo.ExpectedChildren);
                        }

                        particleMatchInfo.Match = ParticleMatch.Partial;
                        return;
                    }
                }

                // all other children constraint are optional.
                particleMatchInfo.Match = ParticleMatch.Matched;
                return;
            }
        }

        // TODO: do better job for simple sequence ( children are elements only ).

        /// <summary>
        /// Get the required elements - elements which minOccurs > 0.
        /// </summary>
        /// <param name="result"></param>
        /// <returns>True if there are required elements in this particle.</returns>
        public override bool GetRequiredElements(ExpectedChildren? result)
        {
            bool requiredElements = false;

            if (ParticleConstraint.MinOccurs > 0)
            {
                foreach (var constraint in ParticleConstraint.ChildrenParticles)
                {
                    if (constraint.ParticleValidator is not null && constraint.ParticleValidator.GetRequiredElements(result))
                    {
                        requiredElements = true;
                        break; // return the first required element in sequence.
                    }
                }
            }

            return requiredElements;
        }

        /// <summary>
        /// Get the expected elements - elements which minOccurs >= 0.
        /// </summary>
        /// <param name="result"></param>
        /// <returns>True if there are expected elements in this particle.</returns>
        public override bool GetExpectedElements(ExpectedChildren result)
        {
            if (ParticleConstraint.ChildrenParticles.Length > 0)
            {
                // sequence, return only the first child.
                ParticleConstraint.ChildrenParticles[0].ParticleValidator?.GetExpectedElements(result);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
