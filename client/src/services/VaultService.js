import { AppState } from "../AppState"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"
import { logger } from "../utils/Logger"
import { Keep, VaultKept } from "../models/Keep"

class VaultsService {
    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        logger.log('vaulting vault', response.data)
        const newVault = new Vault(response.data)
        AppState.vaults.unshift(newVault)
        return newVault
    }
    async deleteVault(vaultId) {
        const response = await api.delete(`api/vaults/${vaultId}`)
        logger.log('Vault, deleted', response.data)
        const indexToRemove = AppState.vaults.findIndex(vault => vault.id == vaultId)
        AppState.vaults.splice(indexToRemove, 1)
    }
    async getVaultById(vaultId) {
        AppState.activeVault = null
        const response = await api.get(`api/vaults/${vaultId}`)
        logger.log("getting vault", response.data)
        AppState.activeVault = new Vault(response.data)
    }
    async getVaultKeeps(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log("getting keeps", response.data)
        let vaultKeeps = response.data.map(keep => new VaultKept(keep))
        AppState.keeps = vaultKeeps
    }
}

export const vaultsService = new VaultsService()