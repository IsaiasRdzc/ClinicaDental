# Repository Structure

This repository follows a protected branch structure to ensure code quality and stability. Below are the branch protection rules and workflow that each contributor must follow.

## Main Branches

- **`main`**: Contains the production-ready code. Any changes to this branch must be fully tested and approved.
- **`development`**: Continuous integration branch. Here, changes from all contribution branches are merged before being promoted to `main`. This branch is also protected.

Both branches (`main` and `development`) have the same protection settings to ensure that only approved and tested changes are integrated.

### Branch Protection Rules

1. **Require a Pull Request to Merge**: All commits must be made in a non-protected branch and then submitted as a Pull Request to `development` or `main`.
2. **Required Approvals**: Every Pull Request must be approved by at least 2 reviewers before it can be merged.
3. **Dismiss Stale Pull Request Approvals**: If new commits are pushed to an existing Pull Request, previous approvals are dismissed, requiring the code to be reviewed again.
4. **Required Status Checks**: All status checks must pass before allowing a merge.
5. **Up-to-Date Branch Requirement**: The Pull Request must be up-to-date with the latest changes in the target branch.
6. **No Bypass Allowed**: These rules apply to everyone, including administrators and custom roles, and cannot be bypassed.

## Workflow

1. **Create a Contribution Branch**: Each contributor should create their own branch from `development`. The branch name should reflect the module or functionality being worked on (e.g., `feature/login` or `bugfix/authentication-issue`).
   
2. **Development and Commits**: Changes should be made in the respective contribution branch. Commit changes incrementally, ensuring they meet code standards.

3. **Create a Pull Request**: Once development in the contribution branch is complete, create a Pull Request to `development`. The Pull Request must:
   - Pass all status checks.
   - Be reviewed and approved by at least 2 reviewers.
   
4. **Merge to `development`**: After receiving approvals and passing checks, the code can be merged into `development`.

5. **Promotion to `main`**: When `development` is ready to go to production, a Pull Request is created to merge it into `main`, following the same review and approval rules.
