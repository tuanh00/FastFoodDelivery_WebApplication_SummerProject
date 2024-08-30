import { createSlice } from "@reduxjs/toolkit";

const initialValue = null;

const userAccount = createSlice({
  name: "account",
  initialState: initialValue,
  reducers: {
    login: (state, action) => action.payload,
    logout: (state, action) => initialValue,
  },
});

export const { login, logout } = userAccount.actions;
export default userAccount.reducer;
