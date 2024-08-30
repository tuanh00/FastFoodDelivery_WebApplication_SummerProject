import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

// Thunk to fetch order history
export const fetchOrderHistory = createAsyncThunk(
  'orderHistory/fetchOrderHistory',
  async (userId, thunkAPI) => {
    const response = await axios.get(`https://localhost:7173/api/Orders/ViewAllOrderByUserID/${userId}`);
    return response.data;
  }
);

const orderHistorySlice = createSlice({
  name: 'orderHistory',
  initialState: {
    orders: [], 
    status: 'idle',
    error: null,
  },
  reducers: {
    updateOrderStatus: (state, action) => {
      const { orderId, status } = action.payload;
      const existingOrder = state.orders.find(order => order.orderId === orderId);
      if (existingOrder) {
        existingOrder.statusOrder = status;
      }
    },
  },
  extraReducers: builder => {
    builder
      .addCase(fetchOrderHistory.pending, state => {
        state.status = 'loading';
      })
      .addCase(fetchOrderHistory.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.orders = action.payload.data || [];
      })
      .addCase(fetchOrderHistory.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message;
      });
  },
});

export const { updateOrderStatus } = orderHistorySlice.actions;
export default orderHistorySlice.reducer;