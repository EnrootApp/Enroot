type Props = {
  children: string | JSX.Element | JSX.Element[];
};

const Bold: React.FC<Props> = (props) => {
  return <span style={{ fontWeight: "bold" }}>{props.children}</span>;
};

export default Bold;
