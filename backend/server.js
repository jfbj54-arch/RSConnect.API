import express from "express";
import cors from "cors";
import dotenv from "dotenv";
import usuariosRoutes from "./routes/usuarios.js";
import prestadoresRoutes from "./routes/prestadores.js";

dotenv.config();

const app = express();

app.use(cors());
app.use(express.json());

app.use("/api/usuarios", usuariosRoutes);
app.use("/api/prestadores", prestadoresRoutes);

app.get("/api", (req, res) => {
    res.json({ status: "API rodando!" });
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => console.log(`Servidor rodando na porta ${PORT}`));
