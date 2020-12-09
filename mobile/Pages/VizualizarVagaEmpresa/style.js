import { StyleSheet } from "react-native";
import Constants from "expo-constants";

export default StyleSheet.create({
  teste: {
    backgroundColor: "#DFDFDF",
  },
  ListaInscricoes: {
    flexDirection: "row",
    justifyContent: "space-around",
    alignItems: "center",
    flexWrap: "wrap",
  },
  imagemCandidato: {
    height: 60,
    width: 60,
    marginTop: 9,
    borderRadius: 20,
  },
  btAprovados: {
    justifyContent: "center",
    alignItems: "center",
    paddingBottom: 20,
  },
  HeaderInscricao: {
    display: "flex",
    alignItems: "center",
    flexDirection: "column",
    width: "100%",
  },
  TextoTitulo: {
    color: "black",
    fontSize: 22,
    textAlign: "center",
    fontWeight: "bold",
    marginTop: "20px",
  },
  BannerVizualizarVagaEmpresa: {
    justifyContent: "center",
    alignItems: "center",
    textAlign: "center",
    height: "300px",
    padding: "5vh",
  },
  TextoHeader: {
    color: "#fff",
    fontSize: 25,
  },
  Vaga: {
    backgroundColor: "#FAFAFA",
    marginBottom: "20px",
    display: "flex",
    flexDirection: "column",
  },
  VagaCompleta: {
    flexDirection: "row",
    borderRadius: 4,
    padding: "3vh",
    flexWrap: "wrap",
    justifyContent: "center",
  },
  MainVaga: {
    display: "flex",
    flexDirection: "column",
    textAlign: "center",
    width: "90%",
  },
  InfoVagas: {
    display: "flex",
    justifyContent: "space-around",
    flexDirection: "row",
    flexWrap: "wrap",
  },
  TecnologiasVaga: {
    display: "flex",
    justifyContent: "space-around",
    flexDirection: "row",
    flexWrap: "wrap",
  },
  ImagemEmpresa: {
    height: "100px",
    width: "100px",
    borderRadius: 100,
  },
  TituloVaga: {
    fontSize: 17,
    textDecorationLine: "underline",
    textDecorationStyle: "solid",
    textDecorationColor: "#000",
  },
  Inscricao: {
    width: "275px",
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    backgroundColor: "white",
    borderRadius: 5,
    marginBottom: "50px",
  },
  AprovarRecusar: {
    flexDirection: "row",
    justifyContent: "space-around",
    width: "100%",
    padding: "2vh",
  },
  btAprovar: {
    backgroundColor: "#00982B",
    height: "36px",
    width: "107px",
    color: "#fff",
    borderWidth: 0,
    borderRadius: 5,
    textAlign: "center",
    justifyContent: "center",
  },
  btReprovar: {
    backgroundColor: "#FD0F00",
    height: "36px",
    width: "107px",
    color: "#fff",
    borderWidth: 0,
    borderRadius: 5,
    textAlign: "center",
    justifyContent: "center",
  },
  btVerAprovados: {
    height: "36px",
    width: "207px",
    padding: "10px",
    backgroundColor: "red",
    marginLeft: "10px",
    alignItems: "center",
  },
  texBtIns: {
    color: "#fff",
    fontWeight: "bold",
  },
  BodyInscricao: {
    textAlign: "center",
    padding: "2vh",
    width: "100%",
  },
  nomeCandidato: {
    borderBottomColor: "black",
    borderWidth: 1,
  },
});
