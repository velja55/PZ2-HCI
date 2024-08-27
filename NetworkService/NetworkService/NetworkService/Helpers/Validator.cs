using NetworkService.Properties;

namespace NetworkService.Helpers
{
    public static class Validator
    {
        public static bool ValidateId(string id, out string errorMessage, out string borderBrush)
        {
            errorMessage = string.Empty;
            borderBrush = Resources.BlackColor;

            if (string.IsNullOrEmpty(id) || id.Equals(Resources.TableViewModel_Id))
            {
                errorMessage = "Id can't be empty!";
                borderBrush = Resources.RedColor;
                return false;
            }
            else if (!int.TryParse(id, out int res))
            {
                errorMessage = "Id must be a number";
                borderBrush = Resources.RedColor;
                return false;
            }
            else if (res < 0)
            {
                errorMessage = "Id must be a positive number";
                borderBrush = Resources.RedColor;
                return false;
            }
            return true;
        }

        public static bool ValidateName(string name, out string errorMessage, out string borderBrush)
        {
            errorMessage = string.Empty;
            borderBrush = "Black";

            if (string.IsNullOrEmpty(name) || name.Equals(Resources.TableViewModel_Name))
            {
                errorMessage = "Name can't be empty!";
                borderBrush = Resources.RedColor;
                return false;
            }
            return true;
        }

        public static bool ValidateType(string type, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(type))
            {
                errorMessage = "You must choose one type";
                return false;
            }
            return true;
        }

        public static bool ValidateSearch(string searchText, out string errorMessage, out string borderBrush)
        {
            errorMessage = string.Empty;
            borderBrush = "Black";

            if (string.IsNullOrEmpty(searchText) || searchText.Equals("Input search here"))
            {
                errorMessage = "Search input can't be empty!";
                borderBrush = Resources.RedColor;
                return false;
            }
            return true;
        }
    }
}
