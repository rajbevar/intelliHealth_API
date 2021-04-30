using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.MedicalEntity
{
   public  class AttributeDto
    {
        //
        // Summary:
        //     Gets and sets the property BeginOffset.
        //     The 0-based character offset in the input text that shows where the attribute
        //     begins. The offset returns the UTF-8 code point in the string.
        public int BeginOffset { get; set; }
        //
        // Summary:
        //     Gets and sets the property EndOffset.
        //     The 0-based character offset in the input text that shows where the attribute
        //     ends. The offset returns the UTF-8 code point in the string.
        public int EndOffset { get; set; }
        //
        // Summary:
        //     Gets and sets the property Id.
        //     The numeric identifier for this attribute. This is a monotonically increasing
        //     id unique within this response rather than a global unique identifier.
        public int Id { get; set; }
        //
        // Summary:
        //     Gets and sets the property RelationshipScore.
        //     The level of confidence that Amazon Comprehend Medical has that this attribute
        //     is correctly related to this entity.
        public float RelationshipScore { get; set; }
        //
        // Summary:
        //     Gets and sets the property Score.
        //     The level of confidence that Amazon Comprehend Medical has that the segment of
        //     text is correctly recognized as an attribute.
        public float Score { get; set; }
        //
        // Summary:
        //     Gets and sets the property Text.
        //     The segment of input text extracted as this attribute.
        [AWSProperty(Min = 1)]
        public string Text { get; set; }
        //
        // Summary:
        //     Gets and sets the property Traits.
        //     Contextual information for this attribute.
        public List<Trait> Traits { get; set; }
        //
        // Summary:
        //     Gets and sets the property Type.
        //     The type of attribute.
        public EntitySubType Type { get; set; }

    }
}
