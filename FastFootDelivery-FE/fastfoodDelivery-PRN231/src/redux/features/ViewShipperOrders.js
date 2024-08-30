import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";

// Thunk to fetch shipper orders
export const fetchShipperOrders = createAsyncThunk(
  "shipperOrders/fetchShipperOrders",
  async (shipperId, thunkAPI) => {
    const response = await axios.get(
      `https://localhost:7173/api/Orders/ViewAllOrderOfShipperID/${shipperId}`
    );
    return response.data;
  }
);

const shipperOrdersSlice = createSlice({
  name: "shipperOrders",
  initialState: {
    orders: [],
    status: "idle",
    error: null,
  },
  reducers: {
    updateOrderDeliveryStatus: (state, action) => {
      const { orderId, status } = action.payload;
      const existingOrder = state.orders.find(
        (order) => order.orderId === orderId
      );
      if (existingOrder) {
        existingOrder.deliveryStatus = status;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchShipperOrders.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchShipperOrders.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.orders = action.payload.data || [];
      })
      .addCase(fetchShipperOrders.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      });
  },
});

export const { updateOrderDeliveryStatus } = shipperOrdersSlice.actions;
export default shipperOrdersSlice.reducer;
