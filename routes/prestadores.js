import express from "express";
import bcrypt from "bcrypt";
import jwt from "jsonwebtoken";
import { db } from "../db.js";

const router = express.Router();

// CADASTRAR PRESTADOR
router.post("/", async (req, res) => {
    const { nome, email, senha, categoria, telefone } = req.body;

    try {
        const hash = await bcrypt.hash(senha, 10);

        const result = await db.query(
            "INSERT INTO prestadores (nome, email, senha, categoria, telefone) VALUES ($1, $2, $3, $4, $5) RETURNING id, nome, email, categoria, telefone",
            [nome, email, hash, categoria, telefone]
        );

        res.json(result.rows[0]);
    } catch (error) {
        res.status(500).json({ error: "Erro ao cadastrar prestador" });
    }
});

// LOGIN PRESTADOR
router.post("/login", async (req, res) => {
    const { email, senha } = req.body;

    try {
        const result = await db.query("SELECT * FROM prestadores WHERE email = $1", [email]);

        if (result.rowCount === 0) {
            return res.json({ error: "Prestador não encontrado" });
        }

        const prestador = result.rows[0];

        const senhaCorreta = await bcrypt.compare(senha, prestador.senha);

        if (!senhaCorreta) {
            return res.json({ error: "Senha incorreta" });
        }

        const token = jwt.sign({ id: prestador.id }, process.env.JWT_SECRET);

        res.json({
            token,
            prestador: {
                id: prestador.id,
                nome: prestador.nome,
                email: prestador.email,
                categoria: prestador.categoria,
                telefone: prestador.telefone
            }
        });
    } catch (error) {
        res.status(500).json({ error: "Erro ao fazer login" });
    }
});

export default router;
