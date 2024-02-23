export class Keep {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.kept = data.kept
        this.creatorId = data.creatorId
        this.creator = data.creator
    }
}

export class VaultKept extends Keep {
    constructor(data) {
        super(data)
        this.vaultKeepId = data.vaultKeepId
    }
}