import { createSlice } from "@reduxjs/toolkit";

const initialValue = [];

const fastFoodCart = createSlice({
  name: "fastfood",
  initialState: initialValue,
  reducers: {
    addFood: (state, action) => {
      const existingItem = state.find((item) => item.id === action.payload.id);
      if (existingItem) {
        existingItem.quantity += 1;
      } else {
        state.push({ ...action.payload, quantity: 1 });
      }
    },

    removeFood: (state, action) => {
      return state.filter((item) => item.id !== action.payload);
    },

    removeAll: (state, action) => initialValue,

    increaseQuantity: (state, action) => {
      const food = state.find((item) => item.id === action.payload);
      if (food) {
        food.quantity += 1;
      }
    },

    decreaseQuantity: (state, action) => {
      const food = state.find((item) => item.id === action.payload);
      if (food) {
        if (food.quantity > 1) {
          food.quantity -= 1;
        } else {
          return state.filter((item) => item.id !== action.payload);
        }
      }
    },
  },
});

export const {
  addFood,
  removeFood,
  increaseQuantity,
  decreaseQuantity,
  removeAll,
} = fastFoodCart.actions;
export default fastFoodCart.reducer;
