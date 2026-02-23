using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// TODO: Implement clothing product class
    /// </summary>
    public class ClothingProduct : Product
    {
        // TODO: Add these properties
        // - Size (string)
        public string Size { get; set; } = string.Empty;
        // - Color (string)
        public string Color { get; set; } = string.Empty;
        // - Material (string)
        public string Material { get; set; } = string.Empty;
        // - Gender (string) - "Men", "Women", "Unisex"
        public string Gender {  get; set; } = string.Empty;
        // - Season (string) - "Summer", "Winter", "All-season"
        public string Season { get; set; } = string.Empty;

        /// <summary>
        /// TODO: Override GetProductDetails for clothing items
        /// </summary>
        public override string GetProductDetails()
        {
            // TODO: Return formatted string with size, color, material
            return $"Size: {Size}, Color: {Color}, Material: {Material}";
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Check if size is available
        /// Valid sizes: XS, S, M, L, XL, XXL
        /// </summary>
        public bool IsValidSize()
        {
            // TODO: Validate size against allowed values
            if(Size== "XS" || Size=="S" || Size=="M" || Size=="L" || Size=="XL" || Size=="XXL")
            {
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Override CalculateValue to apply seasonal discount
        /// Apply 15% discount for off-season items
        /// </summary>
        public override decimal CalculateValue()
        {
            // TODO: Apply seasonal discount logic
            if ((Season == "Summer" && DateTime.Now.Month >= 10) || (Season == "Winter" && DateTime.Now.Month >= 4))
            {
                return Price * Quantity * 0.85m;
            }
            throw new NotImplementedException();
        }
    }
}
