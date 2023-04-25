import { Input, Typography } from "@mui/material";
import { useState } from "react";

interface Props {
  text?: string;
  placeholder?: string;
  multiline?: boolean;
  disabled?: boolean;
  onEditEnd: (text?: string) => void;
}

const InlineEditContainer: React.FC<Props> = ({
  text,
  placeholder,
  multiline = false,
  onEditEnd,
  disabled = false,
}) => {
  const [isEdit, setIsEdit] = useState(false);
  const [editText, setEditText] = useState(text);

  return isEdit && !disabled ? (
    <Input
      value={editText}
      onChange={(e) => setEditText(e.target.value)}
      onBlur={(e) => {
        setIsEdit(false);
        if (editText !== text) {
          onEditEnd(editText);
        }
      }}
      multiline={multiline}
      fullWidth
      autoFocus
    />
  ) : (
    <Typography
      onDoubleClick={() => setIsEdit(true)}
      style={{
        whiteSpace: "pre-line",
        textAlign: "justify",
        wordBreak: "break-word",
      }}
    >
      {editText || placeholder}
    </Typography>
  );
};

export default InlineEditContainer;
