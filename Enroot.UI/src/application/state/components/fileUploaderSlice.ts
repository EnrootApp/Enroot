import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { Attachment } from "../../../domain/tasq/Attachment";

const initialState = [] as Attachment[];

const fileUploaderSlice = createSlice({
  name: "fileUploader",
  initialState,
  reducers: {
    addAttachment(state, action: PayloadAction<Attachment>) {
      return [...state, action.payload];
    },
    reset() {
      return [];
    },
  },
});

export const { addAttachment, reset } = fileUploaderSlice.actions;
export default fileUploaderSlice.reducer;
