using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SiC.Model
{
    public class Restriction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public List<Material> childMaterialRestrictions { get; set; }

        /**
        Restriction algorithm
        If the height is discrete, it should only allow the same measurement to be fit in.
        Else, if the height is continuous, we must check if the value is between the value and valuemax of the dimension.

        The occupation can also be restricted to percentage. E.g, the product must have a minimum fill of 40% and a maximum fill
        of 50%, to be valid.


        Repeat the process for width and depth.
         */
        public static Boolean ProductFits(Product parent, Product child)
        {
            var parentHeight = parent.Dimension.Height;
            var parentWidth = parent.Dimension.Width;
            var parentDepth = parent.Dimension.Depth;

            var childHeight = child.Dimension.Height;
            var childWidth = child.Dimension.Width;
            var childDepth = child.Dimension.Depth;

            //Verify percentage
            if (parent.MaxOccupation > 0)
            {
                //Get parent volume
                double ParentVolume = CalculateParentVolume(parent);

                //Get the volume with the children it currently has
                double ChildrenVolume = CalculateChildrenVolume(parent);

                //And add the volume of the new child we wish to add
                if (child != null)
                    ChildrenVolume += child.Dimension.Height.Value
                                    * child.Dimension.Width.Value
                                    * child.Dimension.Depth.Value;

                //Verify if the occupation exceeds the threshold
                var Occupation = ((ChildrenVolume / ParentVolume) * 100);

                if (Occupation > parent.MaxOccupation) return false;
            }

            //Height verification
            if (parentHeight.isDiscrete)
            {
                if (parentHeight.Value < childHeight.Value) return false;
            }
            else
            {
                //Else it should be between said values
                var parentHeightMax = parent.Dimension.Height.ValueMax;
                var childHeightMax = child.Dimension.Height.ValueMax;
                if (childHeight.Value > parentHeight.Value || childHeightMax > parentHeightMax) return false;
            }

            //Width verification
            if (parentWidth.isDiscrete)
            {
                if (parentWidth.Value < childWidth.Value) return false;
            }
            else
            {
                //Else it should be between said values
                var parentWidthMax = parent.Dimension.Width.ValueMax;
                var childWidthMax = child.Dimension.Width.ValueMax;
                if (childWidth.Value > parentWidth.Value || childWidthMax > parentWidthMax) return false;
            }

            //Depth verification
            if (parentWidth.isDiscrete)
            {
                if (parentHeight.Value < childHeight.Value) return false;
            }
            else
            {
                //Else it should be between said values
                var childDepthMax = child.Dimension.Depth.ValueMax;
                var parentDepthMax = parent.Dimension.Depth.ValueMax;
                if (childDepth.Value > parentDepth.Value || childDepthMax > parentDepthMax) return false;
            }

            return true;
        }

        private static double CalculateParentVolume(Product parent)
        {
            var parentHeight = parent.Dimension.Height.Value;
            var parentWidth = parent.Dimension.Width.Value;
            var parentDepth = parent.Dimension.Depth.Value;

            return parentWidth * parentHeight * parentDepth;
        }

        private static double CalculateChildrenVolume(Product parent)
        {
            double ChildrenVolume = 0;

            foreach (Product child in parent.Components)
            {
                if (child.Dimension != null)
                    ChildrenVolume += child.Dimension.Height.Value
                                    * child.Dimension.Width.Value
                                    * child.Dimension.Depth.Value;
            }

            return ChildrenVolume;
        }

        public static double OccupationPercentage(Product parent)
        {
            return ((CalculateChildrenVolume(parent) / CalculateParentVolume(parent)) * 100);
        }

        public static Boolean ExceedsMinimumRequired(Product parent)
        {
            var Occupation = ((CalculateChildrenVolume(parent) / CalculateParentVolume(parent)) * 100);

            if (Occupation < parent.MinOccupation) return false;
            else return false;
        }
    }
}