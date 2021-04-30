using System;
using System.Collections.Generic;
using System.Text;

namespace PatientEngTranscription.Domain.MedicalEntity
{
   public  class EntityDto
    {
        //
        // Summary:
        //     Gets and sets the property Attributes.
        //     The extracted attributes that relate to this entity.
        public List<Attribute> Attributes { get; set; }
        //
        // Summary:
        //     Gets and sets the property BeginOffset.
        //     The 0-based character offset in the input text that shows where the entity begins.
        //     The offset returns the UTF-8 code point in the string.
        public int BeginOffset { get; set; }
        //
        // Summary:
        //     Gets and sets the property Category.
        //     The category of the entity.
        public EntityType Category { get; set; }
        //
        // Summary:
        //     Gets and sets the property EndOffset.
        //     The 0-based character offset in the input text that shows where the entity ends.
        //     The offset returns the UTF-8 code point in the string.
        public int EndOffset { get; set; }
        //
        // Summary:
        //     Gets and sets the property Id.
        //     The numeric identifier for the entity. This is a monotonically increasing id
        //     unique within this response rather than a global unique identifier.
        public int Id { get; set; }
        //
        // Summary:
        //     Gets and sets the property Score.
        //     The level of confidence that Amazon Comprehend Medical has in the accuracy of
        //     the detection.
        public float Score { get; set; }
        //
        // Summary:
        //     Gets and sets the property Text.
        //     The segment of input text extracted as this entity.
        [AWSProperty(Min = 1)]
        public string Text { get; set; }
        //
        // Summary:
        //     Gets and sets the property Traits.
        //     Contextual information for the entity
        public List<Trait> Traits { get; set; }
        //
        // Summary:
        //     Gets and sets the property Type.
        //     Describes the specific type of entity with category of entities.
        public EntitySubType Type { get; set; }
    }
}
