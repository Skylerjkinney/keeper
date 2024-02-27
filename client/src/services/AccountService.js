import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Vault } from '../models/Vault'
class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async updateAccount(accountData) {
    try {
      logger.log(accountData)
      const response = await api.put('/account', accountData)
      logger.log('Account Updated', response.data)
      const newAccount = new Account(response.data)
      AppState.account = newAccount
    } catch (error) {
      logger.error(error)
    }
  }
  async getMyVaults() {
    const response = await api.get('/account/vaults')
    logger.log('Getting account vaults', response.data)
    const myVaults = response.data.map(vault => new Vault(vault))
    AppState.accountVaults = myVaults
  } catch(error) {
    logger.error(error)
  }
}

export const accountService = new AccountService()
