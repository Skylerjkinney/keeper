import { api } from "./AxiosService"
import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"


class VaultKeepService {
    async createVaultKeep(vaultKeepData) {
        const response = await api.post('api/vaultkeeps', vaultKeepData)
        logger.log('Creating VaultKeep', response.data)
        if (AppState.activeVault?.id == vaultKeepData.vaultId) {
            AppState.vaultKeeps.push(new VaultKeep(response.data))
        }
    }
}

export const vaultKeepService = new VaultKeepService()