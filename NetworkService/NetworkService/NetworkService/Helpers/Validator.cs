namespace NetworkService.Helpers
{
    public static class Validator
    {
        public static bool ValidateId(string id, out string errorMessage, out string borderBrush)
        {
            errorMessage = string.Empty;
            borderBrush = Resources.NetworkService.BlackColor;
            int res;

            if (string.IsNullOrEmpty(id) || id.Equals(Resources.NetworkService.TableViewModel_Id))
            {
                errorMessage = "Id can't be empty!";
                borderBrush = Resources.NetworkService.RedColor;
                return false;
            }
            else if (!int.TryParse(id, out res))
            {
                errorMessage = "Id must be a number";
                borderBrush = Resources.NetworkService.RedColor;
                return false;
            }
            else if (res < 0)
            {
                errorMessage = "Id must be a positive number";
                borderBrush = Resources.NetworkService.RedColor;
                return false;
            }
            return true;
        }

        public static bool ValidateName(string name, out string errorMessage, out string borderBrush)
        {
            errorMessage = string.Empty;
            borderBrush = "Black";

            if (string.IsNullOrEmpty(name) || name.Equals(Resources.NetworkService.TableViewModel_Name))
            {
                errorMessage = "Name can't be empty!";
                borderBrush = Resources.NetworkService.RedColor;
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
                borderBrush = Resources.NetworkService.RedColor;
                return false;
            }
            return true;
        }
    }
}
