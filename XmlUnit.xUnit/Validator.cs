namespace XmlUnit.Xunit
{
    using System.Xml;
    using System.Xml.Schema;

    public class Validator
    {
        private bool hasValidated = false;
        private bool isValid = true;
        private string validationMessage;
        private readonly XmlValidatingReader validatingReader;

        private Validator(XmlReader xmlInputReader)
        {
            validatingReader = new XmlValidatingReader(xmlInputReader);
            validatingReader.ValidationEventHandler += (ValidationEventHandler) ValidationFailed;
        }

        public Validator(XmlInput input) :
            this(input.CreateXmlReader())
        {
        }

        public void ValidationFailed(object sender, ValidationEventArgs e)
        {
            isValid = false;
            validationMessage = e.Message;
        }

        private void Validate()
        {
            if (!hasValidated)
            {
                hasValidated = true;
                while (validatingReader.Read())
                {
                    // only interested in ValidationFailed callbacks
                }
                validatingReader.Close();
            }
        }

        public bool IsValid
        {
            get
            {
                Validate();
                return isValid;
            }
        }

        public string ValidationMessage
        {
            get
            {
                Validate();
                return validationMessage;
            }
        }
    }
}