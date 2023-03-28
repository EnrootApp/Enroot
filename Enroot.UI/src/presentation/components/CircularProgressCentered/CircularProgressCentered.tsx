import { CircularProgress, CircularProgressProps } from "@mui/material";

const CircularProgressCentered = (props: CircularProgressProps) => {
  return (
    <CircularProgress
      {...props}
      sx={{
        position: "absolute",
        top: 0,
        bottom: 0,
        left: 0,
        right: 0,
        margin: "auto",
      }}
    />
  );
};

export default CircularProgressCentered;
