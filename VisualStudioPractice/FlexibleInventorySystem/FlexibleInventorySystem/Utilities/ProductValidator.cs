using FlexibleInventorySystem.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlexibleInventorySystem.Utilities
{
    /// <summary>
    /// TODO: Implement validation helper class
    /// </summary>
    public static class ProductValidator
    {
        /// <summary>
        /// TODO: Validate product data
        /// Check:
        /// - ID not null/empty
        /// - Name not null/empty
        /// - Price > 0
        /// - Quantity >= 0
        /// </summary>
        public static bool ValidateProduct(Product product, out string? errorMessage)
        {
            // TODO: Implement validation
            errorMessage = null;
            if (product == null)
            {
                errorMessage = "Product cannot be null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(product.Id))  errorMessage = "Product ID required.";
            else if (string.IsNullOrWhiteSpace(product.Name))  errorMessage = "Product name required.";
            else if (product.Price <= 0)  errorMessage = "Price must be positive.";
            else if (product.Quantity < 0)  errorMessage = "Quantity cannot be negative.";
            return errorMessage == null;

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Validate electronic product specific rules
        /// </summary>
        public static bool ValidateElectronicProduct(ElectronicProduct product, out string? errorMessage)
        {
            // TODO: Implement electronic validation
            errorMessage = null;
           
            if (product.WarrantyMonths < 0) errorMessage = "Warranty cannot be negative.";
            return errorMessage == null;

            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Validate grocery product specific rules
        /// </summary>
        public static bool ValidateGroceryProduct(GroceryProduct product, out string? errorMessage)
        {
            // TODO: Implement grocery validation
            errorMessage = null;
            if (product.Weight <= 0) errorMessage = "Weight must be positive.";
            else if (string.IsNullOrWhiteSpace(product.StorageTemperature)) errorMessage = "Storage temperature required.";
            else if (product.ExpiryDate <= product.DateAdded) errorMessage = "Invalid expiry date.";
            return errorMessage == null;
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Validate clothing product specific rules
        /// </summary>
        public static bool ValidateClothingProduct(ClothingProduct product, out string? errorMessage)
        {
            // TODO: Implement clothing validation
            errorMessage = null;
            if (string.IsNullOrWhiteSpace(product.Size)) errorMessage = "Size required.";
            else if (string.IsNullOrWhiteSpace(product.Color)) errorMessage = "Color required.";
            else if (string.IsNullOrWhiteSpace(product.Material)) errorMessage = "Material required.";
            else if (string.IsNullOrWhiteSpace(product.Gender)) errorMessage = "Gender required.";
            else if (string.IsNullOrWhiteSpace(product.Season)) errorMessage = "Season required.";
            throw new NotImplementedException();
        }
    }
}
