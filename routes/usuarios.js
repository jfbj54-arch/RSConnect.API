import express from "express";
import bcrypt from "bcrypt";
import jwt from "jsonwebtoken";
import { db } from "../db.js";

const router = express.Router();

// CADASTRAR CLIENTE
router.post("/", async (req, res) => {
    const { nome, email, senha } = req.body;

    try {
        const hash = await bcrypt.hash(senha, 10);

        const result = await db.query(
            "INSERT INTO usuarios (nome, email, senha) VALUES ($1, $2, $3) RETURNING id, nome, email",
            [nome, email, hash]
        );

        res.json(result.rows[0]);
    } catch (error) {
        res.status(500).json({ error: "Erro ao cadastrar cliente" });
    }
});

// LOGIN CLIENTE
router.post("/login", async (req, res) => {
    const { email, senha } = req.body;

    try {
        const result = await db.query("SELECT * FROM usuarios WHERE email = $1", [email]);

        if (result.rowCount === 0) {
            return res.json({ error: "Usuário não encontrado" });
        }

        const usuario = result.rows[0];

        const senhaCorreta = await bcrypt.compare(senha, usuario.senha);

        if (!senhaCorreta) {
            return res.json({ error: "Senha incorreta" });
        }

        const token = jwt.sign({ id: usuario.id }, process.env.JWT_SECRET);

        res.json({
            token,
            usuario: {
                id: usuario.id,
                nome: usuario.nome,
                email: usuario.email
            }
        });
    } catch (error) {
        res.status(500).json({ error: "Erro ao fazer login" });
    }
});

export default router;
