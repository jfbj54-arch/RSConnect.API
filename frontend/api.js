const API_URL = "https://rsconnectapi-production.up.railway.app/api";

const api = {

    async cadastrarCliente(nome, email, senha) {
        try {
            const response = await fetch(`${API_URL}/usuarios`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ nome, email, senha })
            });

            return await response.json();
        } catch (error) {
            return { error: true };
        }
    },

    async loginCliente(email, senha) {
        try {
            const response = await fetch(`${API_URL}/usuarios/login`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, senha })
            });

            return await response.json();
        } catch (error) {
            return { error: true };
        }
    },

    async cadastrarPrestador(nome, email, senha, categoria, telefone) {
        try {
            const response = await fetch(`${API_URL}/prestadores`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ nome, email, senha, categoria, telefone })
            });

            return await response.json();
        } catch (error) {
            return { error: true };
        }
    },

    async loginPrestador(email, senha) {
        try {
            const response = await fetch(`${API_URL}/prestadores/login`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, senha })
            });

            return await response.json();
        } catch (error) {
            return { error: true };
        }
    }
};
